$(document).ready(function () {
    $("btnCreate").click(function () {
        var saveForm = $("#createForm").serialize();
        $.ajax({
            type: "POST",
            url: "Transaction/Create/",
            data: saveForm,
            success: function () {
                window.location.href = "/Transactions/Index";
            }
        })
    })
})