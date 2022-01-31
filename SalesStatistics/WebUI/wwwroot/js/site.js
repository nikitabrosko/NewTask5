// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function(res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    });
}

jQueryAjaxDelete = (url, id) => {
    if (confirm('Are you sure to delete this record?')) {
        try {
            console.log("1");
            $.ajax({
                type: 'POST',
                url: url,
                success: function (res) {
                    $(id).html(res);
                    console.log("3");
                },
                error: function (err) {
                    console.log(err);
                }
            });
            console.log("2");
        } catch (e) {
            console.log(e);
        }
    }

    return false;
}