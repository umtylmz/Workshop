$(document).ready(function () {
    $.ajax({
        url: "/Home/getStaffNames",
        success: function (data) {
            $.each(data, function (index, value) {
                $("thead tr:nth-child(2)").append("<th scope='col'>" + value.Name + "</th>");
            });

            $(".container div:nth-child(2)").remove();
        },
        error: function () {
            alert("Sistemde bir hata meydana geldi.");
        }
    });

    FillJobList();
    FillJobPoints();

    $("#addJob").click(function () {
        $.ajax({
            url: "/Home/JobAssigner",
            async: false,
            cache:false,
            error: function () {
                alert("Sistemde bir hata meydana geldi.");
            }
        });

        setTimeout(function () {
            FillJobList();
            FillJobPoints();
        }, 500);            
    });

    $("#clearAllData").click(function () {
        $.ajax({
            url: "/Home/ClearAllData",
            success: function () {
                alert("Veriler başarıyla temizlendi.");
                FillJobList();
                FillJobPoints();
            },
            error: function () {
                alert("Sistemde bir hata meydana geldi.");
            }
        });
    });

    function FillJobList() {
        $.ajax({
            url: "/Home/GetStaffJobs",
            success: function (data) {
                $("tbody").html("");

                var count = 1;

                $.each(data, function (index, value) {
                    var trTagCount = $("tbody tr");

                    var date = new Date(parseInt(value.Date.substr(6)));

                    if (count > trTagCount.length) {
                        $("tbody").append("<tr></tr>");
                        $("tbody tr:nth-child(" + count + ")").append("<td scope='col'>" + date.getDate() + "." + date.getMonth() + "." + date.getFullYear() + "</td>");
                    }

                    $("tbody tr:nth-child(" + count + ")").append("<td scope='col'>" + value.JobName + "</td>");

                    if (value.StaffID == 6) {
                        count++;
                    }
                });
            },
            error: function () {
                alert("Sistemde bir hata meydana geldi.");
            }
        });

    }
    function FillJobPoints() {
        $.ajax({
            url: "/Home/GetJobPoints",
            success: function (data) {
                $("thead tr:nth-child(1) .canDelete").remove();

                $.each(data, function (index, value) {
                    $("thead tr:nth-child(1)").append("<th class='canDelete' scope='col'>" + value.JobPoint + "</th>");
                });
            },
            error: function () {
                alert("Sistemde bir hata meydana geldi.");
            }
        });
    }

});