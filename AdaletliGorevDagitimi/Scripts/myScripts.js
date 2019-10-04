$(document).ready(function () {
    $.ajax({
        url: "/Home/getStaffNames",
        success: function (data) {
            $.each(data, function (index, value) {
                $("thead tr").append("<th scope='col'>" + value.Name + "</th>");
            });
        },
        error: function () {
            alert("Sistemde bir hata meydana geldi.");
        }
    });
});