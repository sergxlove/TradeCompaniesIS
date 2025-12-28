const existingUsers = ['admin', 'merchandiser', 'client', 'ivanov_user'];

const countries = [
    { id: 1, name: 'Россия', code: '+7' },
    { id: 2, name: 'США', code: '+1' },
    { id: 3, name: 'Китай', code: '+86' },
    { id: 4, name: 'Германия', code: '+49' },
    { id: 5, name: 'Франция', code: '+33' },
    { id: 6, name: 'Италия', code: '+39' },
    { id: 7, name: 'Испания', code: '+34' },
    { id: 8, name: 'Бразилия', code: '+55' },
    { id: 9, name: 'Индия', code: '+91' },
    { id: 10, name: 'Япония', code: '+81' }
];

document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('registrationForm');
    const loading = document.getElementById('loading');
    const successMessage = document.getElementById('successMessage');
    const formActions = document.getElementById('formActions');

    document.getElementById('country').addEventListener('change', function () {
        const countryId = parseInt(this.value);
        const country = countries.find(c => c.id === countryId);
        if (country) {
            document.getElementById('countryCode').textContent = country.code;
        }
    });

    document.getElementById('password').addEventListener('input', function () {
        checkPasswordStrength(this.value);
    });

    document.getElementById('confirmPassword').addEventListener('input', function () {
        validatePasswordMatch();
    });

    document.getElementById('username').addEventListener('blur', function () {
        validateUsername(this.value);
    });

    document.getElementById('email').addEventListener('blur', function () {
        validateEmail(this.value);
    });

    document.getElementById('phone').addEventListener('blur', function () {
        validatePhone(this.value);
    });

    form.addEventListener('submit', async function (e) {
        e.preventDefault();

        if (validateForm()) {
            formActions.style.display = 'none';
            loading.style.display = 'block';

            await new Promise(resolve => setTimeout(resolve, 2000));

            const registrationResult = registerClient();

            loading.style.display = 'none';

            if (registrationResult.success) {
                form.style.display = 'none';
                successMessage.style.display = 'block';

                document.getElementById('clientId').textContent = registrationResult.clientId;
                document.getElementById('loginUsername').textContent =
                    document.getElementById('username').value;
            }
        }
    });
});

function checkPasswordStrength(password) {
    const strengthBar = document.getElementById('passwordStrength');
    let strength = 0;

    if (password.length >= 8) strength++;

    if (/\d/.test(password)) strength++;

    if (/[a-z]/.test(password) && /[A-Z]/.test(password)) strength++;

    if (/[^a-zA-Z0-9]/.test(password)) strength++;

    strengthBar.className = 'strength-bar';
    if (password.length === 0) {
        strengthBar.style.width = '0%';
    } else {
        switch (strength) {
            case 1:
                strengthBar.classList.add('strength-weak');
                break;
            case 2:
            case 3:
                strengthBar.classList.add('strength-medium');
                break;
            case 4:
                strengthBar.classList.add('strength-strong');
                break;
        }
    }
}

function validatePasswordMatch() {
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    const errorElement = document.getElementById('confirmPasswordError');

    if (password && confirmPassword && password !== confirmPassword) {
        showError('confirmPassword', 'Пароли не совпадают');
        return false;
    } else {
        hideError('confirmPassword');
        return true;
    }
}

function validateUsername(username) {
    const errorElement = document.getElementById('usernameError');

    if (username.length < 3) {
        showError('username', 'Логин должен содержать минимум 3 символа');
        return false;
    }

    if (existingUsers.includes(username)) {
        showError('username', 'Этот логин уже занят');
        return false;
    }

    hideError('username');
    return true;
}

function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(email)) {
        showError('email', 'Введите корректный email адрес');
        return false;
    }

    hideError('email');
    return true;
}

function validatePhone(phone) {
    const cleanedPhone = phone.replace(/\D/g, '');

    if (cleanedPhone.length < 10) {
        showError('phone', 'Номер телефона слишком короткий');
        return false;
    }

    hideError('phone');
    return true;
}

function validateForm() {
    let isValid = true;

    const requiredFields = ['lastName', 'firstName', 'birthDate', 'phone',
        'email', 'country', 'address', 'username',
        'password', 'confirmPassword'];

    requiredFields.forEach(fieldId => {
        const field = document.getElementById(fieldId);
        if (!field.value.trim()) {
            showError(fieldId, 'Это поле обязательно для заполнения');
            isValid = false;
        }
    });

    const birthDate = new Date(document.getElementById('birthDate').value);
    const today = new Date();
    const minDate = new Date();
    minDate.setFullYear(today.getFullYear() - 18);

    if (birthDate > minDate) {
        showError('birthDate', 'Вы должны быть старше 18 лет');
        isValid = false;
    }

    if (!document.getElementById('agreeTerms').checked ||
        !document.getElementById('agreePrivacy').checked) {
        alert('Необходимо принять все соглашения');
        isValid = false;
    }

    if (!validateUsername(document.getElementById('username').value)) isValid = false;
    if (!validateEmail(document.getElementById('email').value)) isValid = false;
    if (!validatePhone(document.getElementById('phone').value)) isValid = false;
    if (!validatePasswordMatch()) isValid = false;

    return isValid;
}

function showError(fieldId, message) {
    const field = document.getElementById(fieldId);
    const errorElement = document.getElementById(fieldId + 'Error');

    field.classList.add('error');
    errorElement.textContent = message;
    errorElement.style.display = 'block';
}

function hideError(fieldId) {
    const field = document.getElementById(fieldId);
    const errorElement = document.getElementById(fieldId + 'Error');

    field.classList.remove('error');
    errorElement.style.display = 'none';
}

function resetForm() {
    if (confirm('Вы уверены, что хотите очистить все поля формы?')) {
        document.getElementById('registrationForm').reset();
        document.getElementById('countryCode').textContent = '+7';

        document.querySelectorAll('.error').forEach(el => el.classList.remove('error'));
        document.querySelectorAll('.error-message').forEach(el => el.style.display = 'none');
        document.getElementById('passwordStrength').className = 'strength-bar';
        document.getElementById('passwordStrength').style.width = '0%';
    }
}

function registerClient() {
    const formData = {
        lastName: document.getElementById('lastName').value,
        firstName: document.getElementById('firstName').value,
        middleName: document.getElementById('middleName').value,
        birthDate: document.getElementById('birthDate').value,
        phone: document.getElementById('phone').value,
        email: document.getElementById('email').value,
        countryId: document.getElementById('country').value,
        countryName: document.getElementById('country').options[document.getElementById('country').selectedIndex].text,
        address: document.getElementById('address').value,

        username: document.getElementById('username').value,
        password: document.getElementById('password').value
    };

    const encryptedPhone = encryptData(formData.phone);
    const encryptedEmail = encryptData(formData.email);

    const hashedPassword = hashPassword(formData.password);

    const clientId = Math.floor(Math.random() * 1000) + 100;

    console.log('=== РЕГИСТРАЦИЯ КЛИЕНТА ===');
    console.log('1. Создание записи в таблице Клиент:');
    console.log('   ID клиента:', clientId);
    console.log('   ФИО:', `${formData.lastName} ${formData.firstName} ${formData.middleName}`);
    console.log('   Дата рождения:', formData.birthDate);
    console.log('   Телефон (зашифрован):', encryptedPhone);
    console.log('   Email (зашифрован):', encryptedEmail);
    console.log('   Страна ID:', formData.countryId);
    console.log('   Адрес:', formData.address);
    console.log('');
    console.log('2. Создание записи в таблице login:');
    console.log('   Логин:', formData.username);
    console.log('   Пароль (хеширован):', hashedPassword.substring(0, 50) + '...');
    console.log('   ID клиента:', clientId);
    console.log('');
    console.log('3. Назначение роли "Клиент"');
    console.log('4. Генерация ключей шифрования для клиента');

    return {
        success: true,
        clientId: clientId
    };
}

function encryptData(data) {
    return 'ENCRYPTED:' + btoa(data).substring(0, 30) + '...';
}

function hashPassword(password) {
    const salt = Math.random().toString(36).substring(2, 15);
    const hash = btoa(password + salt).substring(0, 64);
    return salt + ':' + hash;
}

function goToLogin() {
    alert('Переход к странице входа (login.html)');
}