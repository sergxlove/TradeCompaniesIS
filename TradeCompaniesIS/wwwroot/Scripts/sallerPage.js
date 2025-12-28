const mockData = {
    products: [
        { id: "#", name: "Промышленный вентилятор", description: "Вентилятор с высокой производительностью", markup: 25, stock: 105, purchases: 1 },
        { id: "#", name: "Кран-балка", description: "Кран-балка с грузоподъемностью до 5 тонн", markup: 40, stock: 95, purchases: 5 },
        { id: "#", name: "Лазерный уровень", description: "Лазерный уровень для точного выравнивания", markup: 18, stock: 115, purchases: 3 },
        { id: "#", name: "Печь для термообработки", description: "Печь для термообработки металлов", markup: 60, stock: 75, purchases: 0 },
        { id: "#", name: "Анализатор газов", description: "Анализатор газов для контроля выбросов", markup: 90, stock: 1, purchases: 1 }
    ],

    suppliers: [
        { id: "#", name: "Торговая компания 'Север'", country: "Россия", phone: "+7 123 456 78 90", address: "Москва, ул. Примерная, д. 1" },
        { id: "#", name: "Global Supplies Inc.", country: "США", phone: "+1 234 567 89 01", address: "Нью-Йорк, ул. Примерная, д. 2" },
        { id: "#", name: "China Trade Co.", country: "Китай", phone: "+86 123 456 78 90", address: "Пекин, ул. Примерная, д. 3" }
    ],

    warehouses: [
        { id: "#", address: "Москва, ул. Ленина, д. 1", country: "Россия" },
        { id: "#", address: "Санкт-Петербург, ул. Невский пр., д. 2", country: "Россия" }
    ],

    newPrices: [
        { productName: "Промышленный вентилятор", currentPrice: 25000, newPrice: 26500 },
        { productName: "Кран-балка", currentPrice: 45000, newPrice: 43000 }
    ]
};

let currentSupply = {
    items: [],
    supplierId: null,
    warehouseId: null
};

document.addEventListener('DOMContentLoaded', function () {
    initializeNavigation();

    loadProducts();
    loadSuppliers();
    loadDeleteProducts();

    populateSelects();
});

function initializeNavigation() {
    const navItems = document.querySelectorAll('.nav-item');

    navItems.forEach(item => {
        item.addEventListener('click', function () {
            navItems.forEach(i => i.classList.remove('active'));
            this.classList.add('active');

            const pageId = this.getAttribute('data-page');
            document.querySelectorAll('.page').forEach(page => page.classList.remove('active'));
            document.getElementById(pageId + '-page').classList.add('active');
        });
    });
}

function showModal(modalId) {
    document.getElementById(modalId).style.display = 'flex';
}

function closeModal(modalId) {
    document.getElementById(modalId).style.display = 'none';
}

function showAddProductModal() {
    showModal('addProductModal');
}

function showAddSupplierModal() {
    showModal('addSupplierModal');
}

function loadProducts() {
    const tbody = document.getElementById('products-table');
    tbody.innerHTML = '';

    mockData.products.forEach(product => {
        const row = document.createElement('tr');
        row.innerHTML = `
                        <td>${product.id}</td>
                        <td><strong>${product.name}</strong></td>
                        <td>${product.description}</td>
                        <td><span class="status-badge status-info">${product.markup}%</span></td>
                        <td>${product.stock} шт.</td>
                        <td>
                            <button class="btn btn-small btn-primary" onclick="editProduct(${product.id})">Изменить</button>
                        </td>
                    `;
        tbody.appendChild(row);
    });
}

function searchProducts() {
    const searchTerm = document.getElementById('product-search').value.toLowerCase();

    const filtered = mockData.products.filter(product =>
        product.name.toLowerCase().includes(searchTerm) ||
        product.description.toLowerCase().includes(searchTerm)
    );

    const tbody = document.getElementById('products-table');
    tbody.innerHTML = '';

    filtered.forEach(product => {
        const row = document.createElement('tr');
        row.innerHTML = `
                        <td>${product.id}</td>
                        <td><strong>${product.name}</strong></td>
                        <td>${product.description}</td>
                        <td><span class="status-badge status-info">${product.markup}%</span></td>
                        <td>${product.stock} шт.</td>
                        <td>
                            <button class="btn btn-small btn-primary" onclick="editProduct(${product.id})">Изменить</button>
                        </td>
                    `;
        tbody.appendChild(row);
    });
}

function addNewProduct() {
    const name = document.getElementById('new-product-name').value;
    const description = document.getElementById('new-product-description').value;
    const markup = parseInt(document.getElementById('new-product-markup').value);

    if (!name || !description) {
        alert('Заполните обязательные поля');
        return;
    }

    const newProduct = {
        id: Math.max(...mockData.products.map(p => p.id)) + 1,
        name: name,
        description: description,
        markup: markup,
        stock: 0,
        purchases: 0
    };

    mockData.products.push(newProduct);
    loadProducts();
    closeModal('addProductModal');
    alert(`Товар "${name}" добавлен`);
}

function editProduct(productId) {
    const product = mockData.products.find(p => p.id === productId);
    if (product) {
        const newMarkup = prompt(`Введите новую наценку для товара "${product.name}":`, product.markup);
        if (newMarkup !== null) {
            const markup = parseInt(newMarkup);
            if (!isNaN(markup) && markup >= 0 && markup <= 500) {
                product.markup = markup;
                loadProducts();
                alert('Наценка изменена');
            } else {
                alert('Введите корректное значение наценки (0-500%)');
            }
        }
    }
}

function populateSelects() {
    const supplierSelect = document.getElementById('supply-supplier');
    supplierSelect.innerHTML = '<option value="">Выберите поставщика...</option>';
    mockData.suppliers.forEach(supplier => {
        const option = document.createElement('option');
        option.value = supplier.id;
        option.textContent = `${supplier.name} (${supplier.country})`;
        supplierSelect.appendChild(option);
    });

    const warehouseSelect = document.getElementById('supply-warehouse');
    warehouseSelect.innerHTML = '<option value="">Выберите склад...</option>';
    mockData.warehouses.forEach(warehouse => {
        const option = document.createElement('option');
        option.value = warehouse.id;
        option.textContent = `${warehouse.address}`;
        warehouseSelect.appendChild(option);
    });

    const productSelect = document.getElementById('add-product-select');
    productSelect.innerHTML = '<option value="">Выберите товар...</option>';
    mockData.products.forEach(product => {
        const option = document.createElement('option');
        option.value = product.id;
        option.textContent = `${product.name} (ID: ${product.id})`;
        productSelect.appendChild(option);
    });
}

function addProductToSupply() {
    const productId = parseInt(document.getElementById('add-product-select').value);
    const quantity = parseInt(document.getElementById('add-product-quantity').value);
    const price = parseInt(document.getElementById('add-product-price').value);

    if (!productId || !quantity || !price) {
        alert('Заполните все поля');
        return;
    }

    if (price < 10) {
        alert('Цена поставки не может быть ниже 10 рублей');
        return;
    }

    const product = mockData.products.find(p => p.id === productId);
    if (!product) {
        alert('Товар не найден');
        return;
    }

    const existingItem = currentSupply.items.find(item => item.productId === productId);

    if (existingItem) {
        existingItem.quantity += quantity;
    } else {
        currentSupply.items.push({
            productId: productId,
            productName: product.name,
            quantity: quantity,
            price: price
        });
    }

    updateSupplyItemsDisplay();

    document.getElementById('add-product-select').value = '';
    document.getElementById('add-product-quantity').value = 1;
    document.getElementById('add-product-price').value = '';
}

function updateSupplyItemsDisplay() {
    const tbody = document.getElementById('supply-items');
    tbody.innerHTML = '';

    if (currentSupply.items.length === 0) {
        tbody.innerHTML = `
                        <tr>
                            <td colspan="5" class="loading">Добавьте товары в поставку</td>
                        </tr>
                    `;
        return;
    }

    currentSupply.items.forEach((item, index) => {
        const total = item.quantity * item.price;
        const row = document.createElement('tr');
        row.innerHTML = `
                        <td>${item.productName}</td>
                        <td>${item.quantity} шт.</td>
                        <td>${formatCurrency(item.price)}</td>
                        <td>${formatCurrency(total)}</td>
                        <td><button class="btn btn-small btn-danger" onclick="removeSupplyItem(${index})">×</button></td>
                    `;
        tbody.appendChild(row);
    });
}

function removeSupplyItem(index) {
    currentSupply.items.splice(index, 1);
    updateSupplyItemsDisplay();
}

function createSupply() {
    const supplierId = parseInt(document.getElementById('supply-supplier').value);
    const warehouseId = parseInt(document.getElementById('supply-warehouse').value);

    if (!supplierId || !warehouseId) {
        alert('Выберите поставщика и склад');
        return;
    }

    if (currentSupply.items.length === 0) {
        alert('Добавьте хотя бы один товар в поставку');
        return;
    }

    for (const item of currentSupply.items) {
        if (item.price < 10) {
            alert(`Цена товара "${item.productName}" ниже минимальной (10 руб.)`);
            return;
        }
    }

    currentSupply.items.forEach(item => {
        const product = mockData.products.find(p => p.id === item.productId);
        if (product) {
            product.stock += item.quantity;
        }
    });

    alert(`Поставка успешно создана! Товары добавлены на склад.`);

    clearSupplyForm();

    loadProducts();
}

function clearSupplyForm() {
    currentSupply.items = [];
    updateSupplyItemsDisplay();
    document.getElementById('supply-supplier').value = '';
    document.getElementById('supply-warehouse').value = '';
}

function loadSuppliers() {
    const tbody = document.getElementById('suppliers-table');
    tbody.innerHTML = '';

    mockData.suppliers.forEach(supplier => {
        const row = document.createElement('tr');
        row.innerHTML = `
                        <td>${supplier.id}</td>
                        <td><strong>${supplier.name}</strong></td>
                        <td>${supplier.country}</td>
                        <td>${supplier.phone}</td>
                        <td>${supplier.address}</td>
                    `;
        tbody.appendChild(row);
    });
}

function addNewSupplier() {
    const name = document.getElementById('new-supplier-name').value;
    const phone = document.getElementById('new-supplier-phone').value;
    const countryId = document.getElementById('new-supplier-country').value;
    const address = document.getElementById('new-supplier-address').value;

    if (!name || !phone || !address) {
        alert('Заполните обязательные поля');
        return;
    }

    const countryMap = {
        '1': 'Россия',
        '2': 'США',
        '3': 'Китай'
    };

    const newSupplier = {
        id: Math.max(...mockData.suppliers.map(s => s.id)) + 1,
        name: name,
        phone: phone,
        country: countryMap[countryId] || 'Россия',
        address: address
    };

    mockData.suppliers.push(newSupplier);
    loadSuppliers();
    populateSelects();
    closeModal('addSupplierModal');
    alert(`Поставщик "${name}" добавлен`);
}

function loadDeleteProducts() {
    const tbody = document.getElementById('delete-table');
    tbody.innerHTML = '';

    mockData.products.forEach(product => {
        const canDelete = product.purchases < 2;
        const statusText = canDelete ? 'Можно удалить' : 'Нельзя удалить';
        const statusClass = canDelete ? 'status-danger' : 'status-success';

        const row = document.createElement('tr');
        row.innerHTML = `
                        <td>${product.id}</td>
                        <td><strong>${product.name}</strong></td>
                        <td>${product.purchases} покупок</td>
                        <td><span class="status-badge ${statusClass}">${statusText}</span></td>
                        <td>
                            ${canDelete ?
                `<button class="btn btn-small btn-danger" onclick="deleteProduct(${product.id})">Удалить</button>` :
                '<button class="btn btn-small" disabled>Удалить</button>'
            }
                        </td>
                    `;
        tbody.appendChild(row);
    });
}

function deleteProduct(productId) {
    const product = mockData.products.find(p => p.id === productId);
    if (!product) return;

    if (product.purchases >= 2) {
        alert('Этот товар нельзя удалить (более 2 покупок)');
        return;
    }

    if (confirm(`Удалить товар "${product.name}"?`)) {
        mockData.products = mockData.products.filter(p => p.id !== productId);
        alert(`Товар "${product.name}" удален`);
        loadProducts();
        loadDeleteProducts();
    }
}

function formatCurrency(amount) {
    return new Intl.NumberFormat('ru-RU', {
        style: 'currency',
        currency: 'RUB',
        minimumFractionDigits: 0
    }).format(amount);
}

function logout() {
    if (confirm("Выйти из системы?")) {
        alert("Вы вышли из системы");
    }
}