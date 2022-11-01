$(document).ready(function () {
});

function Refresh() {
    location.reload();
}
function SubmitNewUser() {
    if (WaiterMasterValidation() == true) {
        document.getElementById("myForm").submit();
    }
}
function WaiterMasterValidation() {
    var strWaiterName = $("#strWaiterName").val();
    if (strWaiterName == "") {
        alert("plese Enter Waiter Name")
        return false;
    }
    var strWaiterPhone = $("#strWaiterPhone").val();
    if (strWaiterPhone == 0 || strWaiterPhone.length != 10) {
        alert("plese enter your valid mobail nuber ")
        return false;
    }
   
    
    

    return true;
}