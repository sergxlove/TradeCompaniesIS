const databaseData = {
    roles: [
        {
            login: "дима_админ",
            role: "admin",
            clientId: null,
            created: "2025-02-15",
            active: true
        },
        {
            login: "ivan_tovar",
            role: "saller",
            clientId: null,
            created: "2025-02-16",
            active: true
        },
        {
            login: "дима_клиент",
            role: "client",
            clientId: null,
            created: "2025-02-17",
            active: true
        },
    ],

    tables: [
        { name: "Клиент", rows: 42, size: "256 KB", owner: "postgres", data: [] },
        { name: "Товар", rows: 35, size: "128 KB", owner: "postgres", data: [] },
        { name: "Заказ", rows: 32, size: "96 KB", owner: "postgres", data: [] },
        { name: "Товар_в_поставке", rows: 138, size: "384 KB", owner: "postgres", data: [] },
        { name: "Товар в заказе", rows: 40, size: "64 KB", owner: "postgres", data: [] },
        { name: "Поставка", rows: 115, size: "512 KB", owner: "postgres", data: [] },
        { name: "Поставщик", rows: 21, size: "64 KB", owner: "postgres", data: [] },
        { name: "Склад", rows: 10, size: "32 KB", owner: "postgres", data: [] },
        { name: "Страна", rows: 10, size: "16 KB", owner: "postgres", data: [] },
        { name: "Ключи_шифрования", rows: 6, size: "32 KB", owner: "postgres", data: [] },
        { name: "Новые_цены", rows: 1, size: "8 KB", owner: "postgres", data: [] },
        { name: "login", rows: 5, size: "16 KB", owner: "postgres", data: [] },
        { name: "access_log", rows: 4, size: "16 KB", owner: "postgres", data: [] },
        { name: "audit_deletions", rows: 5, size: "24 KB", owner: "postgres", data: [] },
        { name: "аудит_удаления_товаров", rows: 18, size: "48 KB", owner: "postgres", data: [] }
    ],

    mockTableData: {
        "Клиент": [
            { "id_клиента": 1, "Фамилия": "Иванов", "Имя": "Иван", "Отчество": "Иванович", "Номер телефона": "+7 912 345 67 89", "Почта": "ivanov@example.com", "id_страны": 1, "Дата рождения": "1970-05-15", "Адрес": "Москва, ул. Ленина, д. 1" },
            { "id_клиента": 2, "Фамилия": "Петров", "Имя": "Петр", "Отчество": "Петрович", "Номер телефона": "+1 234 567 89 01", "Почта": "petrov@example.com", "id_страны": 2, "Дата рождения": "1985-03-22", "Адрес": "Нью-Йорк, ул. 5-я Авеню, д. 2" },
            { "id_клиента": 3, "Фамилия": "Сидоров", "Имя": "Сидор", "Отчество": "Сидорович", "Номер телефона": "+86 123 456 78 90", "Почта": "sidorov@example.com", "id_страны": 3, "Дата рождения": "1960-11-30", "Адрес": "Пекин, ул. Чанъаньцзе, д. 3" },
            { "id_клиента": 38, "Фамилия": "Ермилов", "Имя": "Константин", "Отчество": "Сергеевич", "Номер телефона": "зашифровано", "Почта": "ermilov@google.com", "id_страны": null, "Дата рождения": null, "Адрес": null },
            { "id_клиента": 39, "Фамилия": "Иванов", "Имя": "кек", "Отчество": "Сергеевич", "Номер телефона": "зашифровано", "Почта": "ermilov@google.com", "id_страны": 3, "Дата рождения": "2001-01-01", "Адрес": "Улица Пушкина Дом Колотушкина" }
        ],
        "Товар": [
            { "id_товара": 4, "Наименование": "Промышленный вентилятор", "Описание": "Вентилятор с высокой производительностью для вентиляции.", "Наценка": 25 },
            { "id_товара": 7, "Наименование": "Кран-балка", "Описание": "Кран-балка с грузоподъемностью до 5 тонн.", "Наценка": 40 },
            { "id_товара": 8, "Наименование": "Лазерный уровень", "Описание": "Лазерный уровень для точного выравнивания конструкций.", "Наценка": 18 },
            { "id_товара": 15, "Наименование": "Печь для термообработки", "Описание": "Печь для термообработки металлов, максимальная температура - 1200°C.", "Наценка": 60 },
            { "id_товара": 20, "Наименование": "Анализатор газов", "Описание": "Анализатор газов для контроля выбросов в атмосферу.", "Наценка": 90 }
        ],
        "Заказ": [
            { "id_заказа": 1, "id_клиента": 42, "Дата": "2025-01-15" },
            { "id_заказа": 2, "id_клиента": 42, "Дата": "2025-12-16" },
            { "id_заказа": 3, "id_клиента": 41, "Дата": "2025-01-17" },
            { "id_заказа": 4, "id_клиента": 41, "Дата": "2025-01-18" },
            { "id_заказа": 5, "id_клиента": 42, "Дата": "2024-01-19" }
        ],
        "Товар_в_поставке": [
            { "id_товаров_в_поставке": 4, "id_товара": 4, "Количество": 1, "Дата_поставки": "1990-01-20", "Цена_поставки": 2500, "id_поставки": 2 },
            { "id_товаров_в_поставке": 7, "id_товара": 7, "Количество": 110, "Дата_поставки": "1986-04-18", "Цена_поставки": 5000, "id_поставки": 3 },
            { "id_товаров_в_поставке": 8, "id_товара": 8, "Количество": 115, "Дата_поставки": "1989-09-30", "Цена_поставки": 4500, "id_поставки": 3 },
            { "id_товаров_в_поставке": 9, "id_товара": 9, "Количество": 125, "Дата_поставки": "1991-05-11", "Цена_поставки": 5000, "id_поставки": 3 }
        ],
        "Страна": [
            { "id_страны": 1, "Наименование": "Россия" },
            { "id_страны": 2, "Наименование": "США" },
            { "id_страны": 3, "Наименование": "Китай" },
            { "id_страны": 4, "Наименование": "Германия" },
            { "id_страны": 5, "Наименование": "Франция" }
        ],
        "login": [
            { "login_id": 16, "login": "postgres", "id_клиента": 38, "passwd": null },
            { "login_id": 17, "login": "aboba", "id_клиента": 39, "passwd": "1e7998a6bdeaa0c56f9a6882d48f44c7:de84561cb44514ecd9fcfe550c274993042a74bcb236f2ae5fb8d301173c598e3d6bfe870b9809a2cc27b4b2c9eb462f0aa04e4766d6114a426e79d47413102b" },
            { "login_id": 19, "login": "sidorov", "id_клиента": 41, "passwd": null },
            { "login_id": 20, "login": "дима_клиент", "id_клиента": 42, "passwd": "4689fcfd90ea6cc697320322f1bc0ce1:095af67eefdb48fcaa8b7a91f220bf2db804346423b01b1db9c82e8cdfe6eb521567bc92c1d73ec93d0a2d6034aed33ddf106324afab80a4dbbd8951b20a835e" }
        ]
    }
};

let currentTable = '';
let currentPage = 1;
let pageSize = 10;
let totalPages = 1;

document.addEventListener('DOMContentLoaded', function () {
    initializeNavigation();

    loadRoles();
    loadTables();
});

function logout() {
    if (confirm("Вы уверены, что хотите выйти?")) {
        alert("Вы вышли из системы");
    }
}

function initializeNavigation() {
    const navItems = document.querySelectorAll('.nav-item');
    navItems.forEach(item => {
        item.addEventListener('click', function () {
            navItems.forEach(i => i.classList.remove('active'));
            this.classList.add('active');
            const pageId = this.getAttribute('data-page');
            document.querySelectorAll('.page').forEach(page => page.classList.remove('active'));
            document.getElementById(pageId + '-page').classList.add('active');

            if (pageId !== 'database') {
                document.getElementById('data-view-container').style.display = 'none';
            }
        });
    });
}

function loadRoles() {
    const tbody = document.getElementById('roles-table');
    tbody.innerHTML = '';

    databaseData.roles.forEach(role => {
        const row = document.createElement('tr');
        row.innerHTML = `
                    <td><strong>${role.login}</strong></td>
                    <td><span class="badge ${getRoleBadgeClass(role.role)}">${getRoleName(role.role)}</span></td>
                    <td>${role.clientId ? role.clientId : '-'}</td>
                    <td>${formatDate(role.created)}</td>
                    <td>${role.active ? '<span class="badge badge-success">Активен</span>' : '<span class="badge badge-danger">Неактивен</span>'}</td>
                    <td>
                        <div class="btn-group">
                            <button class="btn btn-sm btn-primary" onclick="changePassword('${role.login}')">🔑 Пароль</button>
                            <button class="btn btn-sm btn-danger" onclick="deleteUser('${role.login}')" ${role.login === 'postgres' ? 'disabled' : ''}>🗑️ Удалить</button>
                        </div>
                    </td>
                `;
        tbody.appendChild(row);
    });
}

function getRoleBadgeClass(role) {
    switch (role) {
        case 'superuser': return 'badge-danger';
        case 'Администратор': return 'badge-warning';
        case 'товаровед': return 'badge-info';
        case 'client_role': return 'badge-success';
        default: return 'badge-info';
    }
}

function getRoleName(role) {
    switch (role) {
        case 'superuser': return 'Суперпользователь';
        case 'Администратор': return 'Администратор';
        case 'товаровед': return 'Товаровед';
        case 'client_role': return 'Клиент';
        default: return role;
    }
}

function searchRoles() {
    const searchTerm = document.getElementById('role-search').value.toLowerCase();
    const rows = document.querySelectorAll('#roles-table tr');

    rows.forEach(row => {
        const text = row.textContent.toLowerCase();
        row.style.display = text.includes(searchTerm) ? '' : 'none';
    });
}

function showCreateRoleModal() {
    document.getElementById('new-user-login').value = '';
    document.getElementById('new-user-password').value = '';
    document.getElementById('new-user-password-confirm').value = '';
    document.getElementById('new-user-role').value = 'client_role';
    document.getElementById('new-user-client-id').value = '';

    showModal('create-role-modal');
}

function createUser() {
    const login = document.getElementById('new-user-login').value;
    const password = document.getElementById('new-user-password').value;
    const confirmPassword = document.getElementById('new-user-password-confirm').value;
    const role = document.getElementById('new-user-role').value;
    const clientId = document.getElementById('new-user-client-id').value;

    if (!login) {
        alert('Введите логин пользователя');
        return;
    }

    if (!password) {
        alert('Введите пароль');
        return;
    }

    if (password !== confirmPassword) {
        alert('Пароли не совпадают');
        return;
    }

    if (password.length < 6) {
        alert('Пароль должен содержать минимум 6 символов');
        return;
    }

    const existingUser = databaseData.roles.find(r => r.login === login);
    if (existingUser) {
        alert('Пользователь с таким логином уже существует');
        return;
    }

    const newUser = {
        login: login,
        role: role,
        clientId: clientId ? parseInt(clientId) : null,
        created: new Date().toISOString().split('T')[0],
        active: true
    };

    databaseData.roles.push(newUser);

    alert(`Пользователь "${login}" успешно создан!`);

    hideModal('create-role-modal');
    loadRoles();
}

function changePassword(login) {
    const user = databaseData.roles.find(r => r.login === login);
    if (!user) return;

    document.getElementById('change-password-login').textContent = login;
    document.getElementById('new-password').value = '';
    document.getElementById('new-password-confirm').value = '';

    showModal('change-password-modal');
}

function saveNewPassword() {
    const login = document.getElementById('change-password-login').textContent;
    const password = document.getElementById('new-password').value;
    const confirmPassword = document.getElementById('new-password-confirm').value;

    if (!password) {
        alert('Введите новый пароль');
        return;
    }

    if (password !== confirmPassword) {
        alert('Пароли не совпадают');
        return;
    }

    if (password.length < 6) {
        alert('Пароль должен содержать минимум 6 символов');
        return;
    }

    alert(`Пароль для пользователя "${login}" успешно изменен!`);

    hideModal('change-password-modal');
}

function deleteUser(login) {
    const user = databaseData.roles.find(r => r.login === login);
    if (!user) return;

    document.getElementById('delete-user-name').textContent = login;
    showModal('delete-user-modal');
}

function confirmDeleteUser() {
    const login = document.getElementById('delete-user-name').textContent;

    databaseData.roles = databaseData.roles.filter(r => r.login !== login);

    alert(`Пользователь "${login}" удален!`);

    hideModal('delete-user-modal');
    loadRoles();
}

function loadTables() {
    const tbody = document.getElementById('tables-table');
    tbody.innerHTML = '';

    databaseData.tables.forEach(table => {
        const row = document.createElement('tr');
        row.innerHTML = `
                    <td><strong>${table.name}</strong></td>
                    <td>${table.rows}</td>
                    <td>${table.size}</td>
                    <td>${table.owner}</td>
                    <td>
                        <button class="btn btn-sm btn-primary" onclick="viewTableData('${table.name}')">
                            👁️ Просмотреть данные
                        </button>
                    </td>
                `;
        tbody.appendChild(row);
    });
}

function searchTables() {
    const searchTerm = document.getElementById('table-search').value.toLowerCase();
    const rows = document.querySelectorAll('#tables-table tr');

    rows.forEach(row => {
        const text = row.textContent.toLowerCase();
        row.style.display = text.includes(searchTerm) ? '' : 'none';
    });
}

function viewTableData(tableName) {
    currentTable = tableName;
    currentPage = 1;

    document.getElementById('data-view-container').style.display = 'block';
    document.getElementById('table-name').textContent = `Таблица: ${tableName}`;

    loadTableData();
}

function loadTableData() {
    const table = document.getElementById('data-table');
    table.innerHTML = '<tr><td colspan="10" class="loading"><div class="spinner"></div>Загрузка данных...</td></tr>';

    setTimeout(() => {
        const data = databaseData.mockTableData[currentTable] || generateMockData(currentTable);

        if (!data || data.length === 0) {
            table.innerHTML = '<tr><td colspan="10" style="text-align: center; padding: 40px; color: var(--text-secondary);">Таблица пуста</td></tr>';
            document.getElementById('page-info').textContent = 'Страница 1 из 1';
            document.getElementById('prev-btn').disabled = true;
            document.getElementById('next-btn').disabled = true;
            return;
        }

        totalPages = Math.ceil(data.length / pageSize);
        const start = (currentPage - 1) * pageSize;
        const end = start + pageSize;
        const pageData = data.slice(start, end);

        const headers = Object.keys(pageData[0] || {});
        let headersHtml = '<tr>';
        headers.forEach(header => {
            headersHtml += `<th>${header}</th>`;
        });
        headersHtml += '</tr>';

        let dataHtml = '';
        pageData.forEach(row => {
            dataHtml += '<tr>';
            headers.forEach(header => {
                let value = row[header];
                if (value === null || value === undefined) {
                    value = '<em style="color: var(--text-secondary);">NULL</em>';
                } else if (typeof value === 'string' && value.includes('зашифровано')) {
                    value = `<span style="color: var(--warning);">${value}</span>`;
                }
                dataHtml += `<td>${value}</td>`;
            });
            dataHtml += '</tr>';
        });

        table.innerHTML = `<thead>${headersHtml}</thead><tbody>${dataHtml}</tbody>`;

        document.getElementById('page-info').textContent = `Страница ${currentPage} из ${totalPages}`;
        document.getElementById('prev-btn').disabled = currentPage <= 1;
        document.getElementById('next-btn').disabled = currentPage >= totalPages;

    }, 500);
}

function generateMockData(tableName) {
    switch (tableName) {
        case "Поставщик":
            return [
                { "id_поставщика": 1, "Наименование": "Торговая компания 'Север'", "Номер телефона": "+7 123 456 78 90", "id_страны": 1, "Адрес": "Москва, ул. Примерная, д. 1" },
                { "id_поставщика": 2, "Наименование": "Global Supplies Inc.", "Номер телефона": "+1 234 567 89 01", "id_страны": 2, "Адрес": "Нью-Йорк, ул. Примерная, д. 2" }
            ];
        case "Склад":
            return [
                { "id_склада": 1, "Адрес": "Москва, ул. Ленина, д. 1", "id_страны": 1 },
                { "id_склада": 2, "Адрес": "Нью-Йорк, ул. 5-я Авеню, д. 2", "id_страны": 2 }
            ];
        default:
            return [
                { "id": 1, "поле1": "значение1", "поле2": "значение2", "поле3": "значение3" },
                { "id": 2, "поле1": "значение4", "поле2": "значение5", "поле3": "значение6" }
            ];
    }
}

function prevPage() {
    if (currentPage > 1) {
        currentPage--;
        loadTableData();
    }
}

function nextPage() {
    if (currentPage < totalPages) {
        currentPage++;
        loadTableData();
    }
}

function refreshDatabase() {
    loadTables();
    alert('Список таблиц обновлен');

    document.getElementById('data-view-container').style.display = 'none';
}

function showModal(modalId) {
    document.getElementById(modalId).classList.add('active');
}

function hideModal(modalId) {
    document.getElementById(modalId).classList.remove('active');
}

function formatDate(dateString) {
    return new Date(dateString + 'T00:00:00').toLocaleDateString('ru-RU');
}