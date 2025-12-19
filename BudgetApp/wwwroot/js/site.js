$(function () {
    function showModal(buttonSelector, placeholder, modalSelector) {
        $(document).on('click', buttonSelector, function () {
            const url = $(this).data('url');
            $.get(url, function (data) {
                $(placeholder).html(data);
                $(modalSelector).modal('show');
            });
        });
    }

    function bindModalSave(options) {
        $(document).on('click', options.dataSaveSelector, function (e) {
            e.preventDefault();
            const $placeholder = $(options.placeholderId);
            const $modal = $(options.modalId);
            const $form = $placeholder.find('form');

            if (!$form.length) return;

            const formData = new FormData($form[0]);
            const method = ($form.attr('method') || 'POST').toUpperCase();
            const url = $form.attr('action') || $form.data('url');

            $.ajax({
                url: url,
                type: method,
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    // If server returned a form (validation errors), replace the modal body.
                    if (/<form[\s>]/i.test(response)) {
                        $placeholder.html(response);
                    } else {
                        $modal.modal('hide');
                        // Simple post-save behavior: refresh the page to show changes.
                        location.reload();
                    }
                },
                error: function () {
                    // Minimal error handling.
                    alert('An error occurred while saving. Please try again.');
                }
            });
        });
    }


// Transactions
    showModal('button[data-toggle="detail-modal"]',
        '#detailModalPlaceholder', '#detailModal');

    showModal('button[data-toggle="create-modal"]',
        '#createModalPlaceholder', '#createModal');

    bindModalSave({
        placeholderId: '#createModalPlaceholder',
        dataSaveSelector: '[data-save="create-modal"]',
        modalId: '#createModal',
    });

    showModal('button[data-toggle="edit-modal"]', '#editModalPlaceholder', '#editModal')

    bindModalSave({
        placeholderId: '#editModalPlaceholder',
        dataSaveSelector: '[data-save="edit-modal"]',
        modalId: '#editModal',
    });

    showModal('button[data-toggle="delete-modal"]', '#deleteModalPlaceholder', '#deleteModal');

// Categories
})