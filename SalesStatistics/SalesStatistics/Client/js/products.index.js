import $ from 'jquery';
import 'bootstrap';
import 'bootstrap-select';
import 'bootstrap-autocomplete';

const filtersProduct = {
    weights: "0-10000",
    costs: "0-500",
    page: 1,
    pageSize: 4,
    sortBy: "Cost",
    sortOrder: "Ascending"
};

function createProductItem(item) {
    return `
                    <tr>
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.weight}</td>
                        <td>${item.cost}</td>
                    </tr>`;
};

$(document).ready(function () {
    getProducts();

    $("#weights").on("change", function () {
        filtersProduct.weights = $(this).val();
        getProducts();
    });

    $("#costs").on("change", function () {
        filtersProduct.costs = $(this).val();
        getProducts();
    });

    $("#sortBy").on("change", function () {
        filtersProduct.sortBy = $(this).val();
        getProducts();
    });

    $("#sortOrder").on("change", function () {
        filtersProduct.sortOrder = $(this).val();
        getProducts();
    });

    $("#pageSize").on("change", function () {
        filtersProduct.pageSize = $(this).val();
        getProducts();
    });
});


function getProducts() {
    $.ajax({
        url: `api/v1/products`,
        data: filtersProduct,
        traditional: true,
        success: function (data, status, xhr) {
            data = data.map(item => {
                const date = new Date(item.date);
                item.date = `${addInitialDate(date.getDate())}.${addInitialDate(date.getMonth() + 1)}.${date.getFullYear()}`;
                return item;
            });

            $("#productsItems").empty().append($.map(data, createProductItem));
            const count = xhr.getResponseHeader('x-total-count');
            addPaginationButtons(filtersProduct.page, count, filtersProduct.pageSize);
        }
    });
};

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
        filtersProduct.page = $(this).data("page");
        getProducts();
    });
}

function addInitialDate(num) {
    if (num < 10) {
        num = '0' + num;
    }
    return num;
}



