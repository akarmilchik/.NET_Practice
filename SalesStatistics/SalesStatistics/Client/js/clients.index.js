import $ from 'jquery';
import 'bootstrap';
import 'bootstrap-select';
import 'bootstrap-autocomplete';

const filtersClient = {
    countries: [],
    ages: "20-30",
    page: 1,
    pageSize: 4,
    sortBy: "Country",
    sortOrder: "Ascending"
};

const filtersCountry = {
    countries: []
}

function createClientItem(item) {
    return `
                    <tr>
                        <td>${item.id}</td>
                        <td>${item.firstName} ${item.lastName}</td>
                        <td>${item.age}</td>
                        <td>${item.countryName}</td>
                    </tr>`;
};

function createCountryItem(item) {
    return `<option value="${item.id}">${item.name}</option>`
};
$(document).ready(function () {
    getClients();
    getCountries();

    $("#ages").on("change", function () {
        filtersClient.ages = $(this).val();
        getClients();
    });

    $("#countries").on("change", function () {
        filtersClient.countries = $(this).val();
        getClients();
    });

    $("#sortBy").on("change", function () {
        filtersClient.sortBy = $(this).val();
        getClients();
    });

    $("#sortOrder").on("change", function () {
        filtersClient.sortOrder = $(this).val();
        getClients();
    });

    $("#pageSize").on("change", function () {
        filtersClient.pageSize = $(this).val();
        getClients();
    });
});


function getClients() {
    $.ajax({
        url: `api/v1/clients`,
        data: filtersClient,
        traditional: true,
        success: function (data, status, xhr) {
            data = data.map(item => {
                const date = new Date(item.date);
                item.date = `${addInitialDate(date.getDate())}.${addInitialDate(date.getMonth() + 1)}.${date.getFullYear()}`;
                return item;
            });

            $("#clientsItems").empty().append($.map(data, createClientItem));
            const count = xhr.getResponseHeader('x-total-count');
            addPaginationButtons(filtersClient.page, count, filtersClient.pageSize);
        }
    });
};

function getCountries() {
    $.ajax({
        url: "api/v1/clients/countries",
        data: filtersCountry,
        traditional: true,
        success: function (data, status) {
            $("#countries").empty().append($.map(data, createCountryItem));
            $("#countries").selectpicker("refresh");
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
        filtersClient.page = $(this).data("page");
        getClients();
    });
}

function addInitialDate(num) {
    if (num < 10) {
        num = '0' + num;
    }
    return num;
}



