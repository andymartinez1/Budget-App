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
            options = options || {};
            $(document).on('click', options.dataSaveSelector, function (e) {
                e.preventDefault();

                const $button = $(this);
                const $placeholder = $(options.placeholderId);
                const $modal = $(options.modalId);
                const $form = $placeholder.find('form');

                if (!$form.length) return;

                // UI: disable button and show simple spinner text
                const originalHtml = $button.html();
                $button.prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Saving...');

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

                            // If server returns JSON with partial update info, apply it; otherwise reload.
                            if (response && typeof response === 'object' && response.partialHtml && response.targetSelector) {
                                $(response.targetSelector).html(response.partialHtml);
                            } else {
                                location.reload();
                            }
                        }
                    },
                    error: function () {
                        alert('An error occurred while saving. Please try again.');
                    },
                    complete: function () {
                        // restore button state
                        $button.prop('disabled', false).html(originalHtml);
                    }
                });
            });
        }


// Transactions
        showModal('button[data-toggle="transaction-detail-modal"]',
            '#detailTransactionModalPlaceholder', '#transactionDetailModal');

        showModal('button[data-toggle="create-transaction-modal"]',
            '#createTransactionModalPlaceholder', '#createTransactionModal');

        bindModalSave({
            placeholderId: '#createTransactionModalPlaceholder',
            dataSaveSelector: '[data-save="create-transaction-modal"]',
            modalId: '#createTransactionModal',
        });

        showModal('button[data-toggle="edit-transaction-modal"]', '#editTransactionModalPlaceholder', '#editTransactionModal')

        bindModalSave({
            placeholderId: '#editTransactionModalPlaceholder',
            dataSaveSelector: '[data-save="edit-transaction-modal"]',
            modalId: '#editTransactionModal',
        });

        showModal('button[data-toggle="delete-transaction-modal"]', '#deleteTransactionModalPlaceholder', '#deleteModal');

// Categories
        showModal('button[data-toggle="create-category-modal"]',
            '#createCategoryModalPlaceholder', '#createCategoryModal');

        bindModalSave({
            placeholderId: '#createCategoryModalPlaceholder',
            dataSaveSelector: '[data-save="create-category-modal"]',
            modalId: '#createCategoryModal',
        });

        showModal('button[data-toggle="edit-category-modal"]', '#editCategoryModalPlaceholder', '#editCategoryModal')

        bindModalSave({
            placeholderId: '#editCategoryModalPlaceholder',
            dataSaveSelector: '[data-save="edit-category-modal"]',
            modalId: '#editCategoryModal',
        });

        showModal('button[data-toggle="delete-category-modal"]', '#deleteCategoryModalPlaceholder', '#deleteCategoryModal');

    }
)