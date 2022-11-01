$(document).ready(function () {
});
function RedyToProcess(ID)
{
    var data = {
        StockOutId:ID
    }
    $.ajax({
        url: "/JQuery/RedyToProcess",
        datatype: "JSON",
        type: "POST",
        data: data,
        success: function (data) {
            if(data==10)
            {
                window.location.reload();
            }
            else {
                alert("Somthing Went Wrong ! Please Try Again");
            }
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function OutOfDelivery(ID) {
    var data = {
        StockOutId: ID,
        FoodItemId:ID
    }
    $.ajax({
        url: "/JQuery/RedyToProcess",
        datatype: "JSON",
        type: "POST",
        data: data,
        success: function (data) {
            if (data == 10) {
                window.location.reload();
            }
            else {
                alert("Somthing Went Wrong ! Please Try Again");
            }
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function Cancel(ID) {
    var a = confirm("Are You Cancel The Order!");
    if (a == true)
    {
        var data = {
            StockOutId: ID,
            OppType: "OrderCancel"
        }
        $.ajax({
            url: "/JQuery/RedyToProcess",
            datatype: "JSON",
            type: "POST",
            data: data,
            success: function (data) {
                if (data == 10) {
                    window.location.reload();
                }
                else {
                    alert("Somthing Went Wrong ! Please Try Again");
                }
            },
            error: function (result) {
                alert("Error");
            }
        });
    }
}
