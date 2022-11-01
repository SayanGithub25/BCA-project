$(document).ready(function () {
    $("#ddlSheduleId").select2();
    $("#ddlItemCategoryId").select2();
    $("#ddlFoodItemId").select2();

});
function OnCatagoryWiseFood() {
    $.ajax({
        url: "/JQuery/GetFoodCatWise",
        datatype: "JSON",
        type: "POST",
        data: { fk_intFoodItemCategoryId: $('#ddlItemCategoryId').val() },
        success: function (data) {
            BindDistrictDropDownList($("#ddlFoodItemId"), data.Data);
        }
    });
}





function BindDistrictDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    console.log(dataCollection);
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].strFoodItemName, dataCollection[i].pk_intFoodItemId);
        }
    }
}
function AddOnclick() {
    if (ValidateOperation() == true) {
        var $row = $('<tr/>');
        $row.append($('<td style=display:none>').html($("#ddlSheduleId option:selected").val()));
        $row.append($('<td style=display:none>').html($("#ddlItemCategoryId option:selected").val()));
        $row.append($('<td style=display:none>').html($("#ddlFoodItemId option:selected").val()));
        $row.append($('<td/>').html($("#ddlSheduleId option:selected").text()));
        $row.append($('<td/>').html($("#ddlItemCategoryId option:selected").text()));
        $row.append($('<td/>').html($("#ddlFoodItemId option:selected").text()));
        $row.append($('<td>').append("<a onclick='EditDetails(this);' class='btn btn-warning' href='#'>Edit</a>"));
        $row.append($('<td>').append("<a onclick='deleteRow(this);' class='btn btn-warning' href='#'>Delete</a> "));
        $("#table>tbody").append($row);
        var shedule = document.getElementById('table');
        for (var i = 1; i < shedule.rows.length; i++) {

            var ddlFoodItemId = 0;
            if (shedule.rows[i].cells[3].innerHTML != '') {
                ddlFoodItemId = shedule.rows[i].cells[3].innerHTML;
            }

            var ddlItemCategoryId = 0;
            if (shedule.rows[i].cells[2].innerHTML != '') {
                ddlItemCategoryId = shedule.rows[i].cells[2].innerHTML;
            }
            var ddlSheduleId = 0;
            if (shedule.rows[i].cells[1].innerHTML != '') {
                ddlSheduleId = shedule.rows[i].cells[1].innerHTML;
            }
            //var DiscountCellAmt = 0;
            //if (shedule.rows[i].cells[4].innerHTML != '') {
            //    DiscountCellAmt = shedule.rows[i].cells[4].innerHTML;
            //}
            //var Rate = 0;
            //if (shedule.rows[i].cells[3].innerHTML != '') {
            //    Rate = shedule.rows[i].cells[3].innerHTML;
            //}
            //var qty = 0;
            //if (shedule.rows[i].cells[2].innerHTML != '') {
            //    qty = shedule.rows[i].cells[2].innerHTML;
            //}
            //TotalAmt = TotalAmt + parseFloat(tblRestrurentItem.rows[i].cells[3].innerHTML * tblRestrurentItem.rows[i].cells[2].innerHTML);
            //DiscountAmt = DiscountAmt + parseFloat(DiscountCellAmt);
            //CGSTTaxAmt = CGSTTaxAmt + ((parseFloat(Rate) * qty) * parseFloat(CGSTCellAmt)) / 100;
            //SGSTTaxAmt = SGSTTaxAmt + ((parseFloat(Rate) * qty) * parseFloat(SGSTCellAmt)) / 100;
            //NetAmtTotal = NetAmtTotal + (parseFloat(NetAmt));
            ////alert(DiscountAmt);
            ////alert(CGSTTaxAmt);
            ////alert(SGSTTaxAmt);
            ////alert(NetAmt);
            $("#ddlSheduleId").val(ddlSheduleId.toFixed(2));
            $("#ddlItemCategoryId").val(ddlItemCategoryId.toFixed(2));

            $("#ddlFoodItemId").val(ddlFoodItemId.toFixed(2));
            //$("#txtCGST").val(CGST);
            //$("#txtSGST").val(SGST);
            //var Amount = parseFloat(d.Data.decDiningRate) + parseFloat(d.Data.decDiningRate) * parseFloat(gst) / 100;

            //$("#txtNetAmount").val(Amount);
        }
        ClearDetails();
    }
}
function EditDetails(r) {
    //var row = parseInt($(r).closest('tr').index());
    //var ddlSheduleId =$("#ddlSheduleId").val($("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(2)").text());
    //  var ddlItemCategoryId= $("#ddlItemCategoryId").val($("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(4)").text());
    //  var ddlFoodItemId =$("#ddlFoodItemId").val($("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(6)").text());

    //  $('#ddlSheduleId').val(ddlSheduleId).trigger('change');
    //  $('#ddlItemCategoryId').val(ddlItemCategoryId).trigger('change');
    //  $("#ddlFoodItemId").val(ddlFoodItemId).trigger('change');





    var row = parseInt($(r).closest('tr').index());
  //  var check = $("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(0)").text();

      var ddlSheduleId = $("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text();
      var ddlItemCategoryId = $("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(2)").text();
      var ddlFoodItemId = $("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(3)").text();
      //var ddlFoodItemId = $("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(4)").text();
     // var decTaxCGSTPer = $("#table>tbody:eq(0) tr:eq(" + row + ") td:eq(5)").text();
      alert(ddlSheduleId);
      alert(ddlItemCategoryId);
      alert(ddlFoodItemId);

      $('#ddlSheduleId').val(ddlSheduleId).trigger('change');
      $('#ddlItemCategoryId').val(ddlItemCategoryId).trigger('change');
     // $("#ddlFoodItemId").val(ddlFoodItemId).trigger('change');
    
    deleteRowWithOutAlert(r);
}
function ClearDetails() {

    $("#ddlFoodItemId").val('0');
}
function deleteRowWithOutAlert(rowNo) {
    $(rowNo).closest('tr').remove();
}
function deleteRow(rowNo) {
    var agree = confirm("Are you sure you want to delete this record?");
    if (agree) {
        $(rowNo).closest('tr').remove();
        return true;
    }
    else {
        return false;
    }
}
function ValidateOperation()
{
    return true;
}

function InsertUpdateShedule() {
    if (ValidateOperation() == true) {
        var ListSheduleWiseFood = [];
        var tblItem = document.getElementById('table');
        var len = tblItem.rows.length - 1;
        for (var i = 0; i < len; i++) {
            ListSheduleWiseFood.push(
                {
                    Fk_SheduleId: $("#table>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").text(),
                    fk_intFoodItemCategoryId: $("#table>tbody:eq(0) tr:eq(" + i + ") td:eq(1)").text(),
                    Fk_intFoodItemId: $("#table>tbody:eq(0) tr:eq(" + i + ") td:eq(2)").text(),
                });
            alert($("#table>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").text());
            alert($("#table>tbody:eq(0) tr:eq(" + i + ") td:eq(1)").text());
            alert($("#table>tbody:eq(0) tr:eq(" + i + ") td:eq(2)").text());
        }
        var _data = JSON.stringify({
            SheduleWiseFood: {
               ListSheduleWiseFood:ListSheduleWiseFood,
            }
        });
    }
    $.ajax({
        url: "/JQuery/InsertUpdateShedule",
        datatype: "JSON",
        type: "POST",
        data: _data,
        success: function (data) {
            if (data) {
              
                //Notiflix.Report.Success('Save Sucessfully !!');

                //setTimeout(function () {

                //    window.location.reload();
                //}, 2000);



            } else {
                alert('Failed. !!');
            }
        },
        error: function (result) {
            alert("Error");
        }
    });
    }


