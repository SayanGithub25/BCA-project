$(document).ready(function () {
    var parts = document.location.href.split("/");
    var id = parts[parts.length - 1];
    if (id > 0)
    {
        EditOnclick(id);
    }
    //InsertUpdateRservice();
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    $('#bdate').val(today);
});
function AddOnclick() {
    if (AddBtnValidation() == true) {

        var TotalAmt = 0;
        var CGSTTaxAmt = 0;
        var SGSTTaxAmt = 0;
        var DiscountAmt = 0;
        var NetAmt = 0;
        var NetAmtTotal = 0;
        var RateAmt = $("#txtRate").val();
        var Qty = $("#txtQTY").val();
        var cgstper = $("#txtCGST").val();
        var sgstper = $("#txtSGST").val();
        var decTaxCGSTAmount = (parseFloat(RateAmt) * Qty) * parseFloat(cgstper) / 100;
        var decTaxSGSTAmount = (parseFloat(RateAmt) * Qty) * parseFloat(sgstper) / 100;
        var pk_intRestrurentServiceDetailsID = $("#pk_intRestrurentServiceDetailsID").val();
        var $row = $('<tr style="text-align:center">');
        $row.append($('<td style=display:none>').html($("#ddlItemId").val()));
        $row.append($('<td/>').html($("#ddlItemId option:selected").text()));
        $row.append($('<td/>').html($("#txtQTY").val()));
        $row.append($('<td/>').html($("#txtRate").val()));
        if ($("#txtDis").val() == '') {
            $row.append($('<td/>').html(0));
        }
        else {
            $row.append($('<td/>').html($("#txtDis").val()));
        }
        $row.append($('<td/>').html($("#txtCGST").val()));
        $row.append($('<td/>').html($("#txtSGST").val()));
        $row.append($('<td/>').html($("#txtAmt").val()));
        $row.append($('<td>').append("<a onclick='EditRestrurentItemDetails(this);' class='btn btn-warning' href='javascript:void(0)'>Edit</a>"));
        $row.append($('<td>').append("<a onclick='deleteRow(this);' class='btn btn-warning' href='javascript:void(0);'>Delete</a>"));
        $row.append($('<td style=display:none>').html(decTaxCGSTAmount));
        $row.append($('<td style=display:none>').html(decTaxSGSTAmount));
        $row.append($('<td style=display:none>').html($("#bdate").val()));
        $row.append($('<td style=display:none>').html($("#ddlTable").val()));
        $row.append($('<td style=display:none>').html($("#txtGuestName").val()));
        $row.append($('<td style=display:none>').html($("#ddlWaiterName").val()));

        $("#AppendTable>tbody").append($row);
        var tblRestrurentItem = document.getElementById('AppendTable');
        for (var i = 1; i < tblRestrurentItem.rows.length; i++) {

            var CGSTCellAmt = 0;
            if (tblRestrurentItem.rows[i].cells[5].innerHTML != '') {
                CGSTCellAmt = tblRestrurentItem.rows[i].cells[5].innerHTML;
            }

            var SGSTCellAmt = 0;
            if (tblRestrurentItem.rows[i].cells[6].innerHTML != '') {
                SGSTCellAmt = tblRestrurentItem.rows[i].cells[6].innerHTML;
            }
            var NetAmt = 0;
            if (tblRestrurentItem.rows[i].cells[7].innerHTML != '') {
                NetAmt = tblRestrurentItem.rows[i].cells[7].innerHTML;
            }
            var DiscountCellAmt = 0;
            if (tblRestrurentItem.rows[i].cells[4].innerHTML != '') {
                DiscountCellAmt = tblRestrurentItem.rows[i].cells[4].innerHTML;
            }
            var Rate = 0;
            if (tblRestrurentItem.rows[i].cells[3].innerHTML != '') {
                Rate = tblRestrurentItem.rows[i].cells[3].innerHTML;
            }
            var qty = 0;
            if (tblRestrurentItem.rows[i].cells[2].innerHTML != '') {
                qty = tblRestrurentItem.rows[i].cells[2].innerHTML;
            }
            TotalAmt = TotalAmt + parseFloat(tblRestrurentItem.rows[i].cells[3].innerHTML * tblRestrurentItem.rows[i].cells[2].innerHTML);
            DiscountAmt = DiscountAmt + parseFloat(DiscountCellAmt);
            CGSTTaxAmt = CGSTTaxAmt + ((parseFloat(Rate) * qty) * parseFloat(CGSTCellAmt)) / 100;
            SGSTTaxAmt = SGSTTaxAmt + ((parseFloat(Rate) * qty) * parseFloat(SGSTCellAmt)) / 100;
            NetAmtTotal = NetAmtTotal + (parseFloat(NetAmt));
            //alert(DiscountAmt);
            //alert(CGSTTaxAmt);
            //alert(SGSTTaxAmt);
            //alert(NetAmt);
            $("#txtTotalAmount").val(TotalAmt.toFixed(2));
            $("#txtTotalDis").val(DiscountAmt);
            $("#txtTotalCGST").val(CGSTTaxAmt.toFixed(2));
            $("#txtTotalSGST").val(SGSTTaxAmt.toFixed(2));
            $("#txtNetAmount").val(NetAmtTotal.toFixed(2));
            //$("#txtCGST").val(CGST);
            //$("#txtSGST").val(SGST);
            //var Amount = parseFloat(d.Data.decDiningRate) + parseFloat(d.Data.decDiningRate) * parseFloat(gst) / 100;

            //$("#txtNetAmount").val(Amount);
        }
        ClearRestrurentItemDetails();
    }
}
function ClearRestrurentItemDetails() {
    $("#ddlItemId").select2('val');
    $('#ddlItemId').val(0).trigger('change');

    //$("#fk_intFoodItemId").val('');
    //$("#strFoodItemName").val('');
    $("#txtQTY").val('');
    $("#txtRate").val('');
    $("#txtDis").val('');
    $("#txtCGST").val('');
    $("#txtSGST").val('');
    $("#txtAmt").val('');
}
function EditRestrurentItemDetails(r) {
    var row = parseInt($(r).closest('tr').index());
    var fk_intFoodItemId = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(0)").text();
    var intQuantity = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(2)").text();
    var rate = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(3)").text();
    var disRate = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(4)").text();
    var decTaxCGSTPer = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(5)").text();
    var decTaxSGSTPer = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(6)").text();
    var decNetAmount = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(7)").text();
    var decTaxCGSTAmount = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(10)").text();
    var decTaxSGSTAmount = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(11)").text();
    var dtServiceBillDate = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(12)").text();
    var fk_intTableBookingId = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(13)").text();
    var strGuestName = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(14)").text();
    var fk_intWaiterId = $("#AppendTable>tbody:eq(0) tr:eq(" + row + ") td:eq(15)").text();
    $('#ddlItemId').val(fk_intFoodItemId).trigger('change');
    $('#txtQTY').val(intQuantity).trigger('change');
    $("#txtDis").val(disRate);
    $("#txtAmt").val(decNetAmount);
    var TotalAmount = $('#txtTotalAmount').val();
    var TotalDiscount = $('#txtTotalDis').val();
    var TotalCGST = $('#txtTotalCGST').val();
    var TotalSGST = $('#txtTotalSGST').val();
    var TotalNetAmt = $('#txtNetAmount').val();
    $('#txtTotalAmount').val(TotalAmount - (intQuantity * rate));
    $('#txtTotalDis').val(TotalDiscount - disRate);

    var tolQtyRate = (intQuantity * rate);
    var totalcgstrate = tolQtyRate * parseFloat(decTaxCGSTPer)
    $('#txtTotalCGST').val(TotalCGST - totalcgstrate / 100);

    var tolQtyRates = (intQuantity * rate);
    var totalsgstrate = tolQtyRates * parseFloat(decTaxSGSTPer)
    $('#txtTotalSGST').val(TotalSGST - totalsgstrate / 100);
    $("#txtNetAmount").val(parseFloat(TotalNetAmt).toFixed(2) - parseFloat(decNetAmount).toFixed(2));
    $('#bdate').val(dtServiceBillDate);
    $('#ddlTable').val(fk_intTableBookingId);
    $('#txtGuestName').val(strGuestName);
    $('#ddlWaiterName').val(fk_intWaiterId).trigger('change');

    deleteRowWithOutAlert(r);

    // TotalAmt = 0;
    //CGSTTaxAmt = 0;
    //SGSTTaxAmt = 0;
    //DiscountAmt = 0;
    //NetAmt = 0;



}
function deleteRowWithOutAlert(rowNo) {
    $(rowNo).closest('tr').remove();
    CalculateNetAmountRestrurentService();
    //ClearPurchaseItemDetails();
}
function deleteRow(rowNo) {
    var agree = confirm("Are you sure you want to delete this record?");
    if (agree) {
        $(rowNo).closest('tr').remove();
        //$("#txtQTY").val('');
        //$("#txtRate").val('');
        //$("#txtDis").val('');
        //$("#txtCGST").val('');
        //$("#txtSGST").val('');
        //$("#txtAmt").val('');
       // AddOnclick();
        CalculateNetAmountRestrurentService();
        ClearRestrurentItemDetails();
        return true;
    } else {
        return false;
    }

}
function AddBtnValidation() {


    //if ($.trim($("#fk_intFoodItemId").val()) == "") {
    //    alert("Item can not be blank!")
    //    return false;
    //}
    //if ($.trim($("#decRate").val()) == "") {
    //    alert("Rate can not be blank!")
    //    return false;
    //}
    //if ($.trim($("#intQuantity").val()) == "") {
    //    alert("Quantity can not be blank!")
    //    return false;
    //}
    return true;
}
function GetFoodItemRate() {
    var ItemId = $("#ddlItemId").select2('val');
    var Quantity = $("#intQuantity").val();
    var Qty = 0;
    var Rate = 0;
    var Disc = 0;
    var decTaxCGSTPer = 0;
    var decTaxSGSTPer = 0;
    var Amount = 0;
    if ($("#intQuantity").val() != '') {
        Qty = $("#intQuantity").val();
    }
    if ($("#decRate").val() != '') {
        Rate = $("#decRate").val();
    }
    if ($("#decDiscount").val() != '') {
        Disc = $("#decDiscount").val();
    }
    if ($("#decTaxCGSTPer").val() != '') {
        decTaxCGSTPer = $("#decTaxCGSTPer").val();
    }
    if ($("#decTaxSGSTPer").val() != '') {
        decTaxSGSTPer = $("#decTaxSGSTPer").val();
    }
    var Amount = 0;
    var TotAmt = parseFloat(Qty) * parseFloat(Rate);
    var TotAmtAfterDiscount = TotAmt - Disc;
    var decTaxCGSTAmount = (TotAmtAfterDiscount * parseFloat(decTaxCGSTPer)) / 100;
    var decTaxSGSTAmount = (TotAmtAfterDiscount * parseFloat(decTaxSGSTPer)) / 100;
    $.ajax({
        url: "/JQuery/GetFoodItemDetails",
        datatype: "JSON",
        type: "POST",
        data: { pk_intFoodItemId: $("#ddlItemId").select2('val') },
        success: function (d) {
            var rate = d.Data.decDiningRate;
            $("#txtRate").val(rate);
            var gst = d.Data.decGSTTaxRate;
            var CGST = parseFloat(gst) / 2;
            var SGST = parseFloat(gst) / 2;
            $("#txtCGST").val(CGST);
            $("#txtSGST").val(SGST);
            var Amount = parseFloat(d.Data.decDiningRate) + parseFloat(d.Data.decDiningRate) * parseFloat(gst) / 100;
            $("#decTaxCGSTAmount").val((parseFloat(d.Data.decDiningRate) * CGST) / 100);
            $("#decTaxSGSTAmount").val((parseFloat(d.Data.decDiningRate) * SGST) / 100);
            // $("#txtAmt").focus();
        },
        failure: function () {
            alert('something wrong happen');
        }
    });
}
function GetQueItemRate() {
    var dis = 0;
    var txtQTY = $("#txtQTY").val();
    var rate = $("#txtRate").val();
    var totalAmt = txtQTY * rate;
    var cgst = $("#txtCGST").val();
    var sgst = $("#txtSGST").val();
    var TolGst = parseFloat(cgst) + parseFloat(sgst)
    dis = $("#txtDis").val();
    var gstamt = (totalAmt * TolGst) / 100;
    var NetAmt = totalAmt + gstamt - dis;
    $("#txtAmt").val(NetAmt);
}
function InsertUpdateRservice() {
    if (AddBtnValidation() == true) {
        var kotno = $("#kotno").val();
        var billno = $("#billno").val();
         var billno = $("#billno").val();
        var bdate = $("#bdate").val();
        var strGuestName = $("#txtGuestName").val();
        var fk_intWaiterId = $("#ddlWaiterName").val();
        var ListRserviceBill = [];
        var tblItem = document.getElementById('AppendTable');
        var len = tblItem.rows.length - 1;
        for (var i = 0; i < len; i++) {
            ListRserviceBill.push(
                {
                    fk_intFoodItemId: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").text(),
                    intQuantity: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(2)").text(),
                    decRate: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(3)").text(),
                    decDiscount: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(4)").text(),
                    decTaxCGSTPer: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(5)").text(),
                    decTaxSGSTPer: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(6)").text(),
                    decNetAmount: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(7)").text(),
                    decTaxCGSTAmount: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(10)").text(),
                    decTaxSGSTAmount: $("#AppendTable>tbody:eq(0) tr:eq(" + i + ") td:eq(11)").text(),
                });
        }
        var _data = JSON.stringify({
            RserviceBill: {
                dtServiceBillDate: $("#bdate").val(),
                fk_intTableBookingId: $("#ddlTable").val(),
                strGuestName: $("#txtGuestName").val(),
                fk_intWaiterId: $("#ddlWaiterName").val(),
                decDiscountAmount: $("#txtTotalDis").val(),
                decTotalTaxCGSTAmount: $("#txtTotalCGST").val(),
                decTotalTaxSGSTAmount: $("#txtTotalSGST").val(),
                decNetAmount: $("#txtNetAmount").val(),
                decTotalAmount: $("#txtTotalAmount").val(),
                decNetAmount: $("#txtNetAmount").val(),
                pk_intRestrurentServiceId: $('#hdnResSerId').val(),
                ListRserviceBill: ListRserviceBill
            }
        });
        $.ajax({
            url: "/JQuery/InsertUpdateRservice",
            contentType: "application/json",
            datatype: "json",
            type: "POST",
            data: _data,
            success: function (data) {
                if (data) {
                    window.location.reload();
                } else {
                    alert('Failed. !!');
                }
            },
            error: function (result) {
                alert("Error");
            }
        });
    }
}
function EditOnclick(id) {
    $.ajax({
        url: "/JQuery/GetFoodItemDetlsById",
        datatype: "JSON",
        type: "POST",
        data: { pk_intRestrurentServiceId: id },
        success: function (json) {
            var TotalAmt = 0;
            var CGSTTaxAmt = 0;
            var SGSTTaxAmt = 0;
            var DiscountAmt = 0;
            var NetAmt = 0;
            var NetAmtTotal = 0;
            $.each(json.Data, function (key, value) {
                
                var RateAmt = value.decRate;
                var Qty = value.intQuantity;
                var cgstper = value.decTaxCGSTPer;
                var sgstper = value.decTaxSGSTPer;
                var decTaxCGSTAmount = (parseFloat(RateAmt) * Qty) * parseFloat(cgstper) / 100;
                var decTaxSGSTAmount = (parseFloat(RateAmt) * Qty) * parseFloat(sgstper) / 100;



                var $row = $('<tr style="text-align:center">');
                $row.append($('<td style=display:none>').html(value.fk_intFoodItemId));
                $row.append($('<td>').html(value.strFoodItemName));
                $row.append($('<td/>').html(value.intQuantity));
                $row.append($('<td/>').html(value.decRate));
                $row.append($('<td/>').html(value.decDiscount));
                $row.append($('<td/>').html(value.decTaxCGSTPer));
                $row.append($('<td/>').html(value.decTaxSGSTPer));
                $row.append($('<td/>').html(value.decNetAmount));
                $row.append($('<td>').append("<a onclick='EditRestrurentItemDetails(this);' class='btn btn-warning' href='#'>Edit</a>"));
                $row.append($('<td>').append("<a onclick='deleteRow(this);' class='btn btn-warning' href='#'>Delete</a>"));
                $row.append($('<td style=display:none>').html(decTaxCGSTAmount));
                $row.append($('<td style=display:none>').html(decTaxSGSTAmount));
                $row.append($('<td style=display:none>').html(value.dtServiceBillDate));
                $row.append($('<td style=display:none>').html(value.fk_intTableBookingId));
                $row.append($('<td style=display:none>').html(value.strGuestName));
                $row.append($('<td style=display:none>').html(value.fk_intWaiterId));
                $("#AppendTable>tbody").append($row);
                $('#hdnResSerId').val(value.pk_intRestrurentServiceId);
            });
            var tblRestrurentItem = document.getElementById('AppendTable');
            for (var i = 1; i < tblRestrurentItem.rows.length; i++) {
                var CGSTCellAmt = 0;
                if (tblRestrurentItem.rows[i].cells[5].innerHTML != '') {
                    CGSTCellAmt = tblRestrurentItem.rows[i].cells[5].innerHTML;
                }
                var SGSTCellAmt = 0;
                if (tblRestrurentItem.rows[i].cells[6].innerHTML != '') {
                    SGSTCellAmt = tblRestrurentItem.rows[i].cells[6].innerHTML;
                }
                var NetAmt = 0;
                if (tblRestrurentItem.rows[i].cells[7].innerHTML != '') {
                    NetAmt = tblRestrurentItem.rows[i].cells[7].innerHTML;
                }
                var DiscountCellAmt = 0;
                if (tblRestrurentItem.rows[i].cells[4].innerHTML != '') {
                    DiscountCellAmt = tblRestrurentItem.rows[i].cells[4].innerHTML;
                }
                var Rate = 0;
                if (tblRestrurentItem.rows[i].cells[3].innerHTML != '') {
                    Rate = tblRestrurentItem.rows[i].cells[3].innerHTML;
                }
                var qty = 0;
                if (tblRestrurentItem.rows[i].cells[2].innerHTML != '') {
                    qty = tblRestrurentItem.rows[i].cells[2].innerHTML;
                }
                TotalAmt = TotalAmt + parseFloat(tblRestrurentItem.rows[i].cells[3].innerHTML * tblRestrurentItem.rows[i].cells[2].innerHTML);
                DiscountAmt = DiscountAmt + parseFloat(DiscountCellAmt);
                CGSTTaxAmt = CGSTTaxAmt + ((parseFloat(Rate) * qty) * parseFloat(CGSTCellAmt)) / 100;
                SGSTTaxAmt = SGSTTaxAmt + ((parseFloat(Rate) * qty) * parseFloat(SGSTCellAmt)) / 100;
                NetAmtTotal = NetAmtTotal + (parseFloat(NetAmt));
                $("#txtTotalAmount").val(TotalAmt.toFixed(2));
                $("#txtTotalDis").val(DiscountAmt);
                $("#txtTotalCGST").val(CGSTTaxAmt.toFixed(2));
                $("#txtTotalSGST").val(SGSTTaxAmt.toFixed(2));
                $("#txtNetAmount").val(NetAmtTotal.toFixed(2));
            }

            ClearRestrurentItemDetails();
        },
        failure: function () {
            alert('something wrong happen');
        }
    });
}

