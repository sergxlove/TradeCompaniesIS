const clientData = {
    id: 1,
    lastName: "Иванов",
    firstName: "Иван",
    middleName: "Иванович",
    phone: "+7 912 345 67 89",
    email: "ivanov@example.com",
    country: "Россия",
    birthdate: "1970-05-15",
    address: "Москва, ул. Ленина, д. 1"
};

const productsData = [
    {
        id: "#",
        name: "Промышленный вентилятор",
        description: "Вентилятор с высокой производительностью для вентиляции",
        category: "equipment",
        price: 25000,
        stock: 105,
        markup: 25,
        minStock: 10
    },
    {
        id: "#",
        name: "Кран-балка",
        description: "Кран-балка с грузоподъемностью до 5 тонн",
        category: "equipment",
        price: 45000,
        stock: 95,
        markup: 40,
        minStock: 5
    },
    {
        id: "#",
        name: "Лазерный уровень",
        description: "Лазерный уровень для точного выравнивания конструкций",
        category: "tools",
        price: 15000,
        stock: 115,
        markup: 18,
        minStock: 15
    },
    {
        id: "#",
        name: "Печь для термообработки",
        description: "Печь для термообработки металлов, максимальная температура - 1200°C",
        category: "equipment",
        price: 120000,
        stock: 75,
        markup: 60,
        minStock: 3
    }
];

const ordersData = [
    {
        id: "#####",
        date: "2025-12-01",
        items: [
            { productId: 7, name: "Кран-балка", quantity: 1, price: 45000 }
        ],
        total: 45000,
        status: "processing"
    },
    {
        id: "#####",
        date: "2025-12-01",
        items: [
            { productId: 4, name: "Промышленный вентилятор", quantity: 2, price: 25000 },
            { productId: 8, name: "Лазерный уровень", quantity: 1, price: 15000 }
        ],
        total: 65000,
        status: "processing"
    }
];

let currentOrder = {
    items: [],
    total: 0
};

document.addEventListener('DOMContentLoaded', function () {
    initializeProfile();

    initializeOrders();

    initializeNavigation();

    initializeProductSearch();

    loadProductsForOrder();
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

            document.querySelectorAll('.page').forEach(page => {
                page.classList.remove('active');
            });

            document.getElementById(pageId + '-page').classList.add('active');
        });
    });
}

function initializeProfile() {
    document.getElementById('profile-name').textContent =
        `${clientData.lastName} ${clientData.firstName} ${clientData.middleName}`;
    document.getElementById('profile-birthdate').textContent = formatDate(clientData.birthdate);
    document.getElementById('profile-phone').textContent = clientData.phone;
    document.getElementById('profile-email').textContent = clientData.email;
    document.getElementById('profile-country').textContent = clientData.country;
    document.getElementById('profile-address').textContent = clientData.address;

    document.getElementById('edit-lastname').value = clientData.lastName;
    document.getElementById('edit-firstname').value = clientData.firstName;
    document.getElementById('edit-middlename').value = clientData.middleName;
    document.getElementById('edit-birthdate').value = clientData.birthdate;
    document.getElementById('edit-phone').value = clientData.phone;
    document.getElementById('edit-email').value = clientData.email;
    document.getElementById('edit-address').value = clientData.address;
}

function toggleEditProfile() {
    const profileView = document.querySelector('.profile-view');
    const editForm = document.getElementById('edit-profile-form');
    const editBtn = document.querySelector('.edit-btn');

    if (editForm.style.display === 'block') {
        profileView.style.display = 'block';
        editForm.style.display = 'none';
        editBtn.textContent = 'Редактировать';
    } else {
        profileView.style.display = 'none';
        editForm.style.display = 'block';
        editBtn.textContent = 'Скрыть';
    }
}

function saveProfile() {
    const newData = {
        lastName: document.getElementById('edit-lastname').value,
        firstName: document.getElementById('edit-firstname').value,
        middleName: document.getElementById('edit-middlename').value,
        birthdate: document.getElementById('edit-birthdate').value,
        phone: document.getElementById('edit-phone').value,
        email: document.getElementById('edit-email').value,
        country: document.getElementById('edit-country').options[document.getElementById('edit-country').selectedIndex].text,
        address: document.getElementById('edit-address').value
    };

    Object.assign(clientData, newData);

    initializeProfile();

    toggleEditProfile();

    alert('Изменения сохранены успешно!');
}

function initializeOrders() {
    const tbody = document.getElementById('orders-table-body');
    tbody.innerHTML = '';

    ordersData.forEach(order => {
        const row = document.createElement('tr');

        const itemsList = order.items.map(item =>
            `${item.name} (${item.quantity} шт.)`
        ).join(', ');

        let statusText, statusClass;
        switch (order.status) {
            case 'delivered':
                statusText = 'Доставлен';
                statusClass = 'status-delivered';
                break;
            case 'processing':
                statusText = 'В обработке';
                statusClass = 'status-processing';
                break;
            case 'pending':
                statusText = 'Ожидает';
                statusClass = 'status-pending';
                break;
            default:
                statusText = 'Неизвестно';
                statusClass = 'status-pending';
        }

        row.innerHTML = `
                    <td>#${order.id.toString().padStart(5, '0')}</td>
                    <td>${formatDate(order.date)}</td>
                    <td>${itemsList}</td>
                    <td>${formatCurrency(order.total)}</td>
                    <td><span class="status-badge ${statusClass}">${statusText}</span></td>
                    <td>
                        <button class="action-btn" onclick="repeatOrder(${order.id})">
                            Повторить
                        </button>
                    </td>
                `;

        tbody.appendChild(row);
    });
}

function repeatOrder(orderId) {
    const order = ordersData.find(o => o.id === orderId);
    if (order) {
        document.querySelectorAll('.nav-item').forEach(i => i.classList.remove('active'));
        document.querySelector('.nav-item[data-page="new-order"]').classList.add('active');

        document.querySelectorAll('.page').forEach(page => page.classList.remove('active'));
        document.getElementById('new-order-page').classList.add('active');

        currentOrder.items = [...order.items];
        updateOrderDisplay();

        alert(`Заказ #${orderId} добавлен для повторения`);
    }
}

function loadProductsForOrder() {
    const container = document.getElementById('order-products-grid');
    container.innerHTML = '';

    productsData.forEach(product => {
        const card = createProductCard(product, true);
        container.appendChild(card);
    });
}

function searchProductsForOrder() {
    const searchTerm = document.getElementById('order-search').value.toLowerCase();
    const container = document.getElementById('order-products-grid');

    if (!searchTerm) {
        loadProductsForOrder();
        return;
    }

    container.innerHTML = '';

    const filteredProducts = productsData.filter(product =>
        product.name.toLowerCase().includes(searchTerm) ||
        product.description.toLowerCase().includes(searchTerm)
    );

    if (filteredProducts.length === 0) {
        container.innerHTML = '<div class="loading">Товары не найдены</div>';
        return;
    }

    filteredProducts.forEach(product => {
        const card = createProductCard(product, true);
        container.appendChild(card);
    });
}

function addToOrder(productId, quantity = 1) {
    const product = productsData.find(p => p.id === productId);
    if (!product) return;

    const existingItem = currentOrder.items.find(item => item.productId === productId);

    if (existingItem) {
        existingItem.quantity += quantity;
    } else {
        currentOrder.items.push({
            productId: product.id,
            name: product.name,
            price: product.price,
            quantity: quantity
        });
    }

    updateOrderDisplay();
}

function removeFromOrder(productId) {
    currentOrder.items = currentOrder.items.filter(item => item.productId !== productId);
    updateOrderDisplay();
}

function updateOrderDisplay() {
    const container = document.getElementById('order-items-container');
    const totalElement = document.getElementById('order-total');

    if (currentOrder.items.length === 0) {
        container.innerHTML = '<div class="loading">Выберите товары для заказа</div>';
        totalElement.textContent = '0';
        return;
    }

    let total = 0;

    container.innerHTML = '';

    currentOrder.items.forEach(item => {
        const itemTotal = item.price * item.quantity;
        total += itemTotal;

        const itemElement = document.createElement('div');
        itemElement.className = 'order-item';
        itemElement.innerHTML = `
                    <div>
                        <strong>${item.name}</strong><br>
                        <small>${formatCurrency(item.price)} × ${item.quantity} шт.</small>
                    </div>
                    <div>
                        <strong>${formatCurrency(itemTotal)}</strong>
                        <span class="remove-item" onclick="removeFromOrder(${item.productId})">×</span>
                    </div>
                `;

        container.appendChild(itemElement);
    });

    totalElement.textContent = formatCurrency(total);
}

function submitOrder() {
    if (currentOrder.items.length === 0) {
        alert('Добавьте товары в заказ');
        return;
    }

    const newOrder = {
        id: ordersData.length + 1,
        date: new Date().toISOString().split('T')[0],
        items: [...currentOrder.items],
        total: currentOrder.items.reduce((sum, item) => sum + (item.price * item.quantity), 0),
        status: 'pending'
    };

    ordersData.unshift(newOrder);

    initializeOrders();

    clearOrder();

    alert(`Заказ #${newOrder.id} успешно оформлен!`);
}

function clearOrder() {
    currentOrder.items = [];
    updateOrderDisplay();
}

function checkAvailability() {
    const searchValue = document.getElementById('availability-search').value.trim();
    const resultsContainer = document.getElementById('availability-results');

    if (!searchValue) {
        resultsContainer.innerHTML = '<div class="loading">Введите ID товара или название для проверки</div>';
        return;
    }

    let product;

    if (!isNaN(searchValue)) {
        product = productsData.find(p => p.id === parseInt(searchValue));
    } else {
        product = productsData.find(p =>
            p.name.toLowerCase().includes(searchValue.toLowerCase())
        );
    }

    if (!product) {
        resultsContainer.innerHTML = '<div class="loading">Товар не найден</div>';
        return;
    }

    let stockStatus, stockClass, deliveryTime;

    if (product.stock > product.minStock * 2) {
        stockStatus = 'В наличии';
        stockClass = 'stock-available';
        deliveryTime = '1-2 дня';
    } else if (product.stock > 0) {
        stockStatus = 'Мало осталось';
        stockClass = 'stock-low';
        deliveryTime = '3-5 дней';
    } else {
        stockStatus = 'Нет в наличии';
        stockClass = 'stock-out';
        deliveryTime = 'ожидается поставка';
    }

    resultsContainer.innerHTML = `
                <div class="availability-card">
                    <h3>${product.name}</h3>
                    <p>${product.description}</p>

                    <div class="availability-info">
                        <div>
                            <div class="profile-label">Текущий остаток</div>
                            <div class="profile-value">${product.stock} шт.</div>
                        </div>
                        <div>
                            <div class="profile-label">Статус</div>
                            <div class="profile-value"><span class="stock-badge ${stockClass}">${stockStatus}</span></div>
                        </div>
                        <div>
                            <div class="profile-label">Срок поставки</div>
                            <div class="profile-value">${deliveryTime}</div>
                        </div>
                        <div>
                            <div class="profile-label">Цена</div>
                            <div class="profile-value">${formatCurrency(product.price)}</div>
                        </div>
                    </div>
                </div>
            `;
}

function initializeProductSearch() {
    displaySearchResults(productsData);
}

function searchProducts() {
    const searchTerm = document.getElementById('product-search').value.toLowerCase();
    const categoryFilter = document.getElementById('category-filter').value;
    const priceFilter = document.getElementById('price-filter').value;
    const stockFilter = document.getElementById('stock-filter').value;

    let results = productsData.filter(product => {
        if (searchTerm && !product.name.toLowerCase().includes(searchTerm) &&
            !product.description.toLowerCase().includes(searchTerm)) {
            return false;
        }

        if (categoryFilter && product.category !== categoryFilter) {
            return false;
        }

        if (priceFilter) {
            const [min, max] = priceFilter === '50000+' ? [50000, Infinity] : priceFilter.split('-').map(Number);
            if (product.price < min || product.price > max) {
                return false;
            }
        }

        if (stockFilter) {
            if (stockFilter === 'available' && product.stock <= 0) {
                return false;
            }
            if (stockFilter === 'low' && product.stock > 10) {
                return false;
            }
        }

        return true;
    });

    displaySearchResults(results);
}

function displaySearchResults(products) {
    const container = document.getElementById('search-results');

    if (products.length === 0) {
        container.innerHTML = '<div class="loading">Товары не найдены</div>';
        return;
    }

    container.innerHTML = '';

    products.forEach(product => {
        const card = createProductCard(product, false);
        container.appendChild(card);
    });
}

function createProductCard(product, forOrder = false) {
    const card = document.createElement('div');
    card.className = 'product-card';

    let stockStatus, stockClass;
    if (product.stock > 10) {
        stockStatus = 'В наличии';
        stockClass = 'stock-available';
    } else if (product.stock > 0) {
        stockStatus = `Осталось: ${product.stock} шт.`;
        stockClass = 'stock-low';
    } else {
        stockStatus = 'Нет в наличии';
        stockClass = 'stock-out';
    }

    card.innerHTML = `
                <div class="product-header">
                    <div>
                        <div class="product-title">${product.name}</div>
                        <div class="product-price">${formatCurrency(product.price)}</div>
                    </div>
                    <span class="stock-badge ${stockClass}">${stockStatus}</span>
                </div>
                <div class="product-description">${product.description}</div>
                <div class="product-stock">
                    <div>
                        <small>ID: ${product.id}</small>
                    </div>
                    ${forOrder ? `
                    <button class="order-btn"
                            onclick="addToOrder(${product.id})"
                            ${product.stock === 0 ? 'disabled' : ''}>
                        Добавить в заказ
                    </button>
                    ` : `
                    <button class="order-btn"
                            onclick="checkProductAvailability(${product.id})">
                        Проверить наличие
                    </button>
                    `}
                </div>
            `;

    return card;
}

function checkProductAvailability(productId) {
    document.getElementById('availability-search').value = productId;
    checkAvailability();

    document.querySelectorAll('.nav-item').forEach(i => i.classList.remove('active'));
    document.querySelector('.nav-item[data-page="availability"]').classList.add('active');

    document.querySelectorAll('.page').forEach(page => page.classList.remove('active'));
    document.getElementById('availability-page').classList.add('active');
}

function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('ru-RU');
}

function formatCurrency(amount) {
    return new Intl.NumberFormat('ru-RU', {
        style: 'currency',
        currency: 'RUB',
        minimumFractionDigits: 0
    }).format(amount);
}