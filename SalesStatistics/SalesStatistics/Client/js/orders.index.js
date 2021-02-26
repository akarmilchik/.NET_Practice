import $ from 'jquery';
import 'bootstrap';
import 'bootstrap-select';
import 'bootstrap-autocomplete';

const filtersOrder = {
    products: [],
    clients: [],
    page: 1,
    pageSize: 4,
    sortBy: "Clients",
    sortOrder: "Ascending",
    dateFrom: "",
    dateTo: ""
};

const filtersClient = {
    countries: []
}

function createOrderItem(item) {
    return `
                    <tr>
                        <td>${item.id}</td>
                        <td>${item.client.firstName} ${item.client.lastName}</td>
                        <td>${item.product.name}</td>
                        <td>${item.product.cost}</td>
                        <td>${item.date.ToShortDateString()}</td>
                    </tr>`;
};

function createClientItem(item) {
    return `<option value="${item.firstName}">${item.lastName}</option>`
};
$(document).ready(function () {
    getOrders();
    getClients();

    $("#products").on("change", function () {
        filtersOrder.products = $(this).val();
        getOrders();
    });

    $("#clients").on("change", function () {
        filtersOrder.clients = $(this).val();
        getOrders();
    });

    $("#sortBy").on("change", function () {
        filtersOrder.sortBy = $(this).val();
        getOrders();
    });

    $("#sortOrder").on("change", function () {
        filtersOrder.sortOrder = $(this).val();
        getOrders();
    });

    $("#dateFrom").on("change", function () {
        filtersOrder.dateFrom = $(this).val();
        getOrders();
    });

    $("#dateTo").on("change", function () {
        filtersOrder.dateTo = $(this).val();
        getOrders();
    });

    $("#pageSize").on("change", function () {
        filtersOrder.pageSize = $(this).val();
        getOrders();
    });
});


function getOrders() {
$.ajax({
    url: `api/v1/orders`,
    data: filtersOrder,
    traditional: true,
    success: function (data, status, xhr) {
        data = data.map(item => {
            const date = new Date(item.date);
            item.date = `${addInitialDate(date.getDate())}.${addInitialDate(date.getMonth() + 1)}.${date.getFullYear()}`;
            return item;
        });

        $("#ordersItems").empty().append($.map(data, createOrderItem));
        const count = xhr.getResponseHeader('x-total-count');
        addPaginationButtons(filtersOrder.page, count, filtersOrder.pageSize);
    }
});
};

function getClients() {
    $.ajax({
        url: "api/v1/clients",
        data: filtersClient,
        traditional: true,
        success: function (data, status) {
            $("#client").empty().append($.map(data, createClientItem));
            $("#client").selectpicker("refresh");
        }
    });
}

function addPaginationButtons(currentPage, totalCount, pageSize) {
    const pageCount = Math.ceil(totalCount / pageSize);
    const buttons = [];

    for (let i = 1; i <= pageCount; i++) {
        const button = $("<li>", { class: "page-item" });
        if (i === currentPage) {
            button.addClass("active");
            button.append($(`<a class="page-link" href="#">${i}<span class="sr-only">(current)</span></a>`))
        }
        else {
            button.append(`<a class="page-link bg-dark text-light" href="#">${i}</a>`);
        }
        button.data("page", i);
        buttons.push(button);
    }
    $(".pagination").empty().append(buttons);
    $(".pagination").addClass("justify-content-center");
    $(".page-item").on("click", function () {
        filtersOrder.page = $(this).data("page");
        getOrders();
    });
}

function addInitialDate(num) {
    if (num < 10)
    {
        num = '0' + num;
    }
    return num;
}



