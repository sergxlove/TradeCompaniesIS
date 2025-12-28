const users = {
    'admin': {
        password: 'admin123',
        role: 'admin',
        name: 'Администратор'
    },
    'merchandiser': {
        password: 'merch123',
        role: 'merchandiser',
        name: 'Товаровед'
    },
    'client': {
        password: 'client123',
        role: 'client',
        name: 'Клиент'
    }
};

let currentUser = null;

document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.getElementById('loginForm');
    const loginBtn = document.getElementById('loginBtn');
    const loading = document.getElementById('loading');
    const errorMessage = document.getElementById('errorMessage');

    loginForm.addEventListener('submit', async function (e) {
        e.preventDefault();

        const username = document.getElementById('username').value.trim();
        const password = document.getElementById('password').value;

        loginBtn.style.display = 'none';
        loading.style.display = 'block';
        errorMessage.style.display = 'none';

        await new Promise(resolve => setTimeout(resolve, 1000));

        const user = users[username];

        if (user && user.password === password) {
            currentUser = {
                username: username,
                ...user
            };

            loginForm.reset();

            if (user.role == 'admin') {
                window.location.href = 'adminPage.html';
            }
            if (user.role == 'merchandiser') {
                window.location.href = 'sallerPage.html';
            }
            if (user.role == 'client') {
                window.location.href = 'clientPage.html';
            }
        } else {
            errorMessage.style.display = 'block';
            loginBtn.style.display = 'block';
            loading.style.display = 'none';

            loginForm.classList.add('error');
            setTimeout(() => loginForm.classList.remove('error'), 500);
        }
    });

    const inputs = document.querySelectorAll('input');
    inputs.forEach(input => {
        input.addEventListener('focus', function () {
            this.parentElement.classList.add('focused');
        });

        input.addEventListener('blur', function () {
            this.parentElement.classList.remove('focused');
        });
    });

    function autoFillCredentials(username) {
        document.getElementById('username').value = username;
        document.getElementById('password').value = users[username]?.password || '';
    }

    document.addEventListener('keydown', function (e) {
        if (e.altKey) {
            switch (e.key) {
                case '1':
                    autoFillCredentials('admin');
                    break;
                case '2':
                    autoFillCredentials('merchandiser');
                    break;
                case '3':
                    autoFillCredentials('client');
                    break;
            }
        }
    });
});

function logout() {
    currentUser = null;
}