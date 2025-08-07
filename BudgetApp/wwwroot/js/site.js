function loadTransactionDetails(id) {
    $.ajax({
        url: `/Transaction/DetailsPartial/${id}`,
        method: 'GET',
        data: {id: id},
        success: function (data) {
            $('#detailModal').modal('show');
            $('#detailModal .modal-body').html(data);
        },
        error: function () {
            $('#detailModal .modal-content').html('<div class="modal-body">Error loading details.</div>');
        }
    });
}