var table = $('#emp_table');

$(document).ready(function () {
    doAjax(function (callback) {
        if (callback == 'success') {
            // add event listeners
        }
    });
});

function doAjax(callback) {
    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: '@Url.Action("GetAll")',
        dataType: 'json',
        success: function (data) {
            // parse json

            return callback('success');
        },
        error: function () {
            return callback('failed');
        }
    });
}