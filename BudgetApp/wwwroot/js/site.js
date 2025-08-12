$(function () {
    function loadTransactionDetails(buttonSelector, placeholder, modalSelector) {
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

    loadTransactionDetails('button[data-toggle="detail-modal"]',
        '#detailModalPlaceholder', '#detailModal');

    editTransaction('button[data-toggle="edit-modal"]', '#editModalPlaceholder', '#editModal');
});