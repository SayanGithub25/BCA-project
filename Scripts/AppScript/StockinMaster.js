$(document).ready(function () {
});
function btnUp(data) {

    var row = $(data).parents("tr");
    var cols = row.children("td");
    $('#hdnFoodItemId').val($(cols[0]).text());
    $("#hdnDeliveryRate").val($(cols[2]).find('input').val());
    $('#hdnTakeAwayRate').val($(cols[3]).find('input').val());
    $('#hdnFoodCount').val($(cols[4]).find('input').val());
    if ($('#hdnDeliveryRate').val() == 0) {
        alert("Please Enter Delivery Rate");
    }
    else if ($('#hdnTakeAwayRate').val() == 0) {
        alert("Please Enter Take Away Rate");
    }
    else {
        document.getElementById('form').submit();
    }
}
