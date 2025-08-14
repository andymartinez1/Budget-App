$(function () {
    function getModalDetails(buttonSelector, placeholder, modalSelector) {
        $(document).on('click', buttonSelector, function () {
            const url = $(this).data('url');
            $.get(url, function (data) {
                $(placeholder).html(data);
                $(modalSelector).modal('show');
            })
        })
    }

    function editTransaction(buttonSelector, placeholder, modalSelector) {
        $(document).on('click', buttonSelector, function () {
            const url = $(this).data('url');
            $.get(url, function (data) {
                $(placeholder).html(data);
                $(modalSelector).modal('show');
            })
        })
    }

    getModalDetails('button[data-toggle="create-modal"]',
        '#createModalPlaceholder', '#createModal');

    getModalDetails('button[data-toggle="detail-modal"]',
        '#detailModalPlaceholder', '#detailModal');

    getModalDetails('button[data-toggle="edit-modal"]', '#editModalPlaceholder', '#editModal')

    getModalDetails('button[data-toggle="delete-modal"]', '#deleteModalPlaceholder', '#deleteModal');
});