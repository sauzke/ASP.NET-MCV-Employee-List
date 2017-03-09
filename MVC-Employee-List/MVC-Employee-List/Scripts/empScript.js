var main = $('#main_table');

$(document).ready(function () {
    doAjax(function (callback) {
        if (callback == "success") {
            $('input[type=radio][name=emp_id]').change(function () {
                $('#edit').attr('href', 'DialogForm.aspx?id=' + this.value);
            });
        }
    });

    $("#delete").click(function () {
        var id = $('input[name=emp_id]:checked').val();
        if (id != null) {
            $.ajax({
                type: 'GET',
                url: 'EmployeeForm.aspx/Delete_One',
                contentType: 'application/json; charset=utf-8',
                data: { 'id': id },
                success: function () {
                    $('tr[name=' + id + ']').hide("slow");
                }
            });
        }
    });
});

function doAjax(callback) {
    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: 'EmployeeForm.aspx/Get_Table',
        success: function (data) {
            var res = JSON.parse(data['d']);
            var str = "";

            for (var i = 0; i < res.length; i++) {
                str += '<tr name="' + res[i].emp_id + '"><td class="radio_col"><input name="emp_id" value="' + res[i].emp_id + '" type="radio"/></td>';
                str += '<td class="info_col">';
                str += '<table class="emp_table">';
                str += '<tr><td>Name:</td><td>' + res[i].emp_name + '</td></tr>';
                str += '<tr><td>Title:</td><td>' + res[i].emp_title + '</td></tr>';
                str += '<tr><td>Date Started:</td><td>' + res[i].emp_start + '</td></tr>';
                str += '<tr><td>Date Ended:</td><td>' + res[i].emp_end + '</td></tr></table></td>';
                str += '<td class="image_col"><img src="' + res[i].emp_image_url + '"/></td></tr>';
            }

            $('#main_table').append(str);

            return callback("success");
        },
        error: function (data) {
            return callback("failed");
        }
    });
}