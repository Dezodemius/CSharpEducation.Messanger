let currentChatId = null;
let currentUserId = null;

function showLoginForm() {
    document.getElementById('login-form').style.display = 'block';
    document.getElementById('register-form').style.display = 'none';
    document.getElementById('guest-form').style.display = 'none';
}

function showRegisterForm() {
    document.getElementById('login-form').style.display = 'none';
    document.getElementById('register-form').style.display = 'block';
    document.getElementById('guest-form').style.display = 'none';
}

function register() {
    const registerUsername = document.getElementById('register-username');
    const registerPassword = document.getElementById('register-password');

    const data = {
        "userName": registerUsername.value, "password": registerPassword.value,
    }
    const response = fetch('/User', {
        method: "POST", headers: {
            "Content-Type": "application/json",
        }, body: JSON.stringify(data),
    }).then(r => {
        if (r.ok) {
            alert('Вы зарегестрированы');
            showLoginForm();
        } else alert('Произошла ошибка');
    });
}

function login() {
    const loginUsername = document.getElementById('login-username');
    const loginPassword = document.getElementById('login-password');

    const data = {
        userName: loginUsername.value, password: loginPassword.value
    };

    fetch('/User/login', {
        method: "POST", headers: {
            "Content-Type": "application/json",
        }, body: JSON.stringify(data),
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Ошибка при входе');
            }
        })
        .then(result => {
            alert('Вы вошли в систему');
            document.querySelector('.auth').style.display = 'none';
            document.querySelector('.messenger').style.display = 'flex';

            currentUserId = result.userId;
        })
        .catch(error => {
            console.error('Ошибка при входе:', error);
            alert('Произошла ошибка при входе');
        });
}

document.addEventListener("DOMContentLoaded", () => {
    const chatList = document.getElementById("chatList");
    const messageList = document.getElementById("messageList");
    const messageInput = document.getElementById("messageInput");
    const sendButton = document.getElementById("sendButton");
    const chatNameInput = document.getElementById("chat-name-input");
    let currentChatId = null;

    function loadUsers() {
        fetch('/User/GetAllUsers/')
            .then(response => response.json())
            .then(data => {
                const userList = document.getElementById("userList");
                userList.innerHTML = '';

                data.forEach(user => {
                    const li = document.createElement('li');
                    const checkbox = document.createElement('input');
                    checkbox.type = 'checkbox';
                    checkbox.value = user.id; // Используем ID пользователя как значение чекбокса

                    const label = document.createElement('label');
                    label.for = user.id;
                    label.innerText = user.userName;


                    li.appendChild(checkbox);
                    li.appendChild(label);

                    userList.appendChild(li);
                });
            })
            .catch(error => console.error('Ошибка при загрузке пользователей:', error));
    }

    function loadChats() {
        fetch('/Chats/UserChats/' + currentUserId)
            .then(response => response.json())
            .then(data => {
                const chatList = document.getElementById("chatList");
                chatList.innerHTML = '';
                data.forEach(chat => {
                    const chatTab = document.createElement("div");
                    chatTab.textContent = chat.name;
                    chatTab.classList.add('chat-tab');

                    chatTab.addEventListener("click", () => {
                        console.log(`Chat clicked: ${chat.id}`);
                        currentChatId = chat.id;
                        loadMessages(currentChatId);

                        let chatTitle = document.getElementById("chatTitle");
                        chatTitle.innerHTML = '';
                        chatTitle.innerText = chat.name;

                        let chatUsers = document.getElementById("chatUserList");
                        chatUsers.innerHTML = '';
                        for (let i = 0; i < chat.users.length; i++) {
                            chatUsers.appendChild(document.createTextNode(chat.users[i].name + " "));
                        }
                    });
                    chatList.appendChild(chatTab);
                });
            })
            .catch(error => console.error('Ошибка при загрузке чатов:', error));
    }

    function createChat() {
        const chatName = chatNameInput.value;
        if (!chatName) {
            return;
        }

        const selectedUsers = [];
        document.querySelectorAll('#userList input[type="checkbox"]:checked').forEach(checkbox => {
            selectedUsers.push(checkbox.value);
        });

        selectedUsers.push(currentUserId);

        const requestBody = {
            name: chatName, userIds: selectedUsers
        };

        fetch('/Chats', {
            method: "POST", headers: {
                "Content-Type": "application/json",
            }, body: JSON.stringify(requestBody),
        })
            .then(response => response.json())
            .then(data => {
                const chatTab = document.createElement("div");
                chatTab.textContent = data.name;
                chatTab.addEventListener("click", () => {
                    currentChatId = data.id;
                    loadMessages(currentChatId);
                });
                chatTab.classList.add('chat-tab');
                chatList.appendChild(chatTab);
                chatNameInput.value = '';
            })
            .catch(error => console.error('Ошибка при создании чата:', error));
    }

    function loadMessages(chatId) {
        if (!chatId) {
            return;
        }
        fetch(`/Messages/${currentChatId}`)
            .then(response => response.json())
            .then(data => {
                messageList.innerHTML = '';
                data.forEach(message => {
                    const messageItem = document.createElement("li");
                    messageItem.textContent = `${message.userName} ${message.dateTime}: ${message.content}`;
                    messageList.appendChild(messageItem);
                });
            })
            .catch(error => console.error('Ошибка при загрузке сообщений:', error));
    }

    sendButton.addEventListener("click", () => {
        if (!currentChatId || !messageInput.value) {
            return;
        }

        const messageData = {
            chatId: currentChatId, content: messageInput.value,
        };

        fetch('/Messages', {
            method: "POST", headers: {
                "Content-Type": "application/json",
            }, body: JSON.stringify(messageData),
        })
            .then(response => response.json())
            .then(data => {

                const messageItem = document.createElement("li");
                messageItem.textContent = `${data.userName} ${data.dateTime}: ${data.content}`;
                messageList.appendChild(messageItem);
                messageInput.value = '';
            })
            .catch(error => console.error('Ошибка при отправке сообщения:', error));
    });

    loadChats();
    loadUsers();
    setInterval(() => loadMessages(currentChatId), 2500);
    setInterval(() => loadChats(), 2500);

    document.getElementById("createChatButton").addEventListener("click", createChat);
});


