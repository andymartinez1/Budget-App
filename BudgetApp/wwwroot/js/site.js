// $(function () {
//     function getModalDetails(buttonSelector, placeholder, modalSelector) {
//         $(document).on('click', buttonSelector, function () {
//             const url = $(this).data('url');
//             $.get(url, function (data) {
//                 $(placeholder).html(data);
//                 $(modalSelector).modal('show');
//             })
//         })
//     }
// });

$(function () {
    var placeHolderElement = $('#createModalPlaceholder')

    $('button[data-bs-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeHolderElement.html(data);
            placeHolderElement.find('.modal').modal('show');
        })
    })

    placeHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            placeHolderElement.find('.modal').modal('hide');
        })
    })
})