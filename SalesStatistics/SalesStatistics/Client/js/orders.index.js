import $ from 'jquery';
import 'bootstrap';
import 'bootstrap-select';
import 'bootstrap-autocomplete';

const filtersOrder = {
    products: [],
    clients: [],
    page: 1,
    pageSize: 4,
    sortBy: "Date",
    sortOrder: "Ascending",
    dateFrom: "",
    dateTo: "",
    searchString: ""
};

const filtersVenue = {
    cities: []
}

function createEventItem(item) {
    return `
                    <tr>
                        <td>${item.Id}</td>
                        <td>${item.Client.FirstName} ${item.Client.LastName}</td>
                        <td>${item.Product.Name}</td>
                        <td>${item.Product.Cost}</td>
                        <td>${item.Date.ToShortDateString()}</td>
                    </tr>`;
};

function createClientItem(item) {
    return `<option value="${item.id}">${item.name}</option>`
};
$(document).ready(function () {
    getProducts();
    getClients();

    $("#cat2").on("change", function () {
        filtersOrder.products = $(this).val();
        getProducts();
    });

    $("#category").on("change", function () {
        filtersOrder.eventCategories = $(this).val();
        getProducts();
    });

    $("#venue").on("change", function () {
        filtersOrder.clients = $(this).val();
        getProducts();
    });

    $("#city").on("change", function () {
        filtersVenue.cities = $(this).val();
        filtersOrder.products = $(this).val();
        filtersOrder.clients = [];
        getClients();
        getProducts();
    });

    $("#sortBy").on("change", function () {
        filtersOrder.sortBy = $(this).val();
        getProducts();
    });

    $("#sortOrder").on("change", function () {
        filtersOrder.sortOrder = $(this).val();
        getProducts();
    });

    $("#dateFrom").on("change", function () {
        filtersOrder.dateFrom = $(this).val();
        getProducts();
    });

    $("#dateTo").on("change", function () {
        filtersOrder.dateTo = $(this).val();
        getProducts();
    });

    $("#pageSize").on("change", function () {
        filtersOrder.pageSize = $(this).val();
        getProducts();
    });

    $("#search").on("click", function () {
        filtersOrder.searchString = $("#autosuggest").val();
        getProducts();
    });

    $('#autosuggest').autoComplete({
        resolverSettings: {
            url: 'api/v1/orders/autosuggest',
        },
    });
});


function getProducts() {
$.ajax({
    url: `api/v1/events`,
    data: filtersOrder,
    traditional: true,
    success: function (data, status, xhr) {
        data = data.map(item => {
            const date = new Date(item.date);
            item.date = `${addInitialDate(date.getDate())}.${addInitialDate(date.getMonth() + 1)}.${date.getFullYear()}`;
            return item;
        });

        $("#productItems").empty().append($.map(data, createEventItem));
        const count = xhr.getResponseHeader('x-total-count');
        addPaginationButtons(filtersOrder.page, count, filtersOrder.pageSize);
    }
});
};

function getClients() {
    $.ajax({
        url: "api/v1/clients",
        data: filtersVenue,
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
        getProducts();
    });
}

function addInitialDate(num) {
    if (num < 10)
    {
        num = '0' + num;
    }
    return num;
}



