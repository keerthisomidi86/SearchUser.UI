$(document).ready(function () {
    var el = document.getElementById("btnSearch");
    el.addEventListener("click", function () {
        $.ajax({
            type: 'POST',
            url: '/user/search',
            data: { searchStr: $("#Search").val() },
            success: function (response) {
                var html = "";
                if (response == null || response == "") {
                    html = "<p> No search input provided. </p>";
                    $("#errorMsg").html(html)
                }
                else {
                    for (var i = 0; i < response.length; i++) {
                        html += "<tr><td id=" + response[i].id + ">" + response[i].id + "</td>"
                            + "<td id=" + response[i].id + ">" + response[i].firstName + "</td>"
                            + "<td id=" + response[i].id + ">" + response[i].lastName + "</td>"
                            + "<td id=" + response[i].id + ">" + response[i].emailAddress + "</td>"
                            + "<td id=" + response[i].id + ">" + response[i].gender + "</td></tr>"
                    }
                    $("#tbody").html(html);
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }, false);
});