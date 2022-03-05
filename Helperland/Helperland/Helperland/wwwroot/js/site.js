////rotate image on accordin
$("#accordion").on("hide.bs.collapse show.bs.collapse", e => {
    $(e.target)
        .prev()
        .find("img:last-child")
        .toggleClass("right rotated");
});

//rotate image in accordin
$("#accordion2").on("hide.bs.collapse show.bs.collapse", e => {
    $(e.target)
        .prev()
        .find("img:last-child")
        .toggleClass("right rotated");
});

//show sidebar menu and overlay
$(document).ready(function () {

    $('.dismiss, .overlay').on('click', function () {
        // hide sidebar
        $('.sidebar').removeClass('active');
        // hide overlay
        $('.overlay').removeClass('active');
    });

    $('.sidebarCollapse').on('click', function () {
        // open sidebar
        $('.sidebar').addClass('active');
        // fade in the overlay
        $('.overlay').addClass('active');
        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });
});

//makes row clickable
jQuery(document).ready(function ($) {
    $(".clickable-row").click(function () {
        window.location = $(this).data("href");
    });
});

// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()
// to popup modal
$(document).ready(function () {

    if (window.location.href.indexOf('#forgot-password-Modal') != -1) {
        $('#forgot-password-Modal').modal('show');
    }

    if (window.location.href.indexOf('#confirm-password-Modal') != -1) {
        $('#confirm-password-Modal').modal('show');
    }

});


function okCookies() {
    $("#cookies").addClass("d-none");
}




function tabOneClick() {
    $("#schedule-service-tab").removeClass("active");
    $("#service-detials-tab").removeClass("active");
    $("#service-payment-tab").removeClass("active")
    $("#schedule-service-tab").removeClass("activen");
    $("#service-detials-tab").removeClass("activen");
    $("#service-payment-tab").removeClass("activen");
    $("#schedule-service-tab").prop("disabled", true);
    $("#service-detials-tab").prop("disabled", true);
    $("#service-payment-tab").prop("disabled", true);

}

function tabtwoClick() {

    $("#service-detials-tab").removeClass("active");
    $("#service-payment-tab").removeClass("active")
    $("#service-detials-tab").removeClass("activen");
    $("#service-payment-tab").removeClass("activen");
    $("#service-detials-tab").prop("disabled", true);
    $("#service-payment-tab").prop("disabled", true);

    printTotalPayment(true);
}

function tabthreeClick() {
    $("#service-payment-tab").removeClass("active")
    $("#service-payment-tab").removeClass("activen");
    $("#service-payment-tab").prop("disabled", true);

}

function printTotalPayment(first = false, whichCheckbox = 0, what = 2) {
    var hourRate = parseFloat(document.getElementById("HourRate").value);
    var hours = parseFloat(document.getElementById("select-service-hour").value);
    var extra1 = 0, extra2 = 0, extra3 = 0, extra4 = 0, extra5 = 0;
    if (first) {
        extra1 = document.getElementById("inside-cabinet").checked ? 0.5 : 0;
        extra2 = document.getElementById("Inside-fridge").checked ? 0.5 : 0;
        extra3 = document.getElementById("Inside-oven").checked ? 0.5 : 0;
        extra4 = document.getElementById("Laundry-wash").checked ? 0.5 : 0;
        extra5 = document.getElementById("Interior-windows").checked ? 0.5 : 0;
    }
    else {

        switch (whichCheckbox) {


            case 1: if (what == 1)
                extra1 = 0.5;
            else if (what == 0)
                extra1 = 0;
                extra2 = document.getElementById("Inside-fridge").checked ? 0.5 : 0;
                extra3 = document.getElementById("Inside-oven").checked ? 0.5 : 0;
                extra4 = document.getElementById("Laundry-wash").checked ? 0.5 : 0;
                extra5 = document.getElementById("Interior-windows").checked ? 0.5 : 0;
                break;

            case 2: if (what == 1)
                extra2 = 0.5;
            else if (what == 0)
                extra2 = 0;
                extra1 = document.getElementById("inside-cabinet").checked ? 0.5 : 0;
                extra3 = document.getElementById("Inside-oven").checked ? 0.5 : 0;
                extra4 = document.getElementById("Laundry-wash").checked ? 0.5 : 0;
                extra5 = document.getElementById("Interior-windows").checked ? 0.5 : 0;
                break;

            case 3: if (what == 1)
                extra3 = 0.5;
            else if (what == 0)
                extra3 = 0;
                extra1 = document.getElementById("inside-cabinet").checked ? 0.5 : 0;
                extra2 = document.getElementById("Inside-fridge").checked ? 0.5 : 0;
                extra4 = document.getElementById("Laundry-wash").checked ? 0.5 : 0;
                extra5 = document.getElementById("Interior-windows").checked ? 0.5 : 0;
                break;

            case 4: if (what == 1)
                extra4 = 0.5;
            else if (what == 0)
                extra4 = 0;
                extra1 = document.getElementById("inside-cabinet").checked ? 0.5 : 0;
                extra2 = document.getElementById("Inside-fridge").checked ? 0.5 : 0;
                extra3 = document.getElementById("Inside-oven").checked ? 0.5 : 0;
                extra5 = document.getElementById("Interior-windows").checked ? 0.5 : 0;
                break;

            case 5: if (what == 1)
                extra5 = 0.5;
            else if (what == 0)
                extra5 = 0;
                extra1 = document.getElementById("inside-cabinet").checked ? 0.5 : 0;
                extra2 = document.getElementById("Inside-fridge").checked ? 0.5 : 0;
                extra3 = document.getElementById("Inside-oven").checked ? 0.5 : 0;
                extra4 = document.getElementById("Laundry-wash").checked ? 0.5 : 0;
                break;
        }
    }


    var totalHour = hours + extra1 + extra2 + extra3 + extra4 + extra5;
    document.getElementById("ForTotalhour").setAttribute("value", totalHour);
    document.getElementById("forHour").innerHTML = hours;
    document.getElementById("totalTime").innerHTML = totalHour.toString() + " Hrs";

    var Payment = totalHour * hourRate;

    document.getElementById("forHourPayment").innerHTML = Payment.toString();
    document.getElementById("forTotalPayment").innerHTML = Payment.toString();

}

function tabOneSuccess() {

    $("#setup-service-tab").removeClass("active");
    $("#setup-service").removeClass("active");
    $("#setup-service").addClass("fade");
    $("#schedule-service").addClass("active");
    $("#schedule-service").removeClass("fade");
    $("#schedule-service-tab").addClass("active");
    $("#schedule-service-tab").addClass("activen");
    $("#schedule-service-tab").prop("disabled", false);

    var input = document.getElementById("service-date");
    var div = document.getElementById("forStartDate");
    div.innerHTML = input.value;
    input = document.getElementById("service-date");
    div = document.getElementById("forStartDate");
    div.innerHTML = input.value;

    printTotalPayment(true);

    $('#schedule-service form').data('validator', null);
    $.validator.unobtrusive.parse('#schedule-service form');
}

function tabOnefaliure(xhr, status) {
    if (xhr.responseText != "") {
        var err = JSON.parse(xhr.responseText);
        if (err.success == false) {

            $('#zipecodeError').html(err.message);
        }


    }
}


function isValidDate(d) {
    return d instanceof Date && !isNaN(d);
}



function validateform2() {
    var selectedTime = document.getElementById("select-service-time").value;
    var selectedDate = document.getElementById("service-date").value;
    var totalHour = parseFloat(document.getElementById("ForTotalhour").value);
    var selecteddate = new Date(selectedDate + "T" + selectedTime);

    if (!isValidDate(selecteddate)) {
        selecteddate = new Date();
        selecteddate.setDate(selecteddate.getDate() + 1);
        var tt = selectedTime.split(":");
        selecteddate.setHours(tt[0]);
        selecteddate.setMinutes(tt[1]);
        selecteddate.setSeconds(00);

    }

    var selecteddate2 = new Date(selecteddate);
    var currentdate = new Date();
    if (selecteddate.getDate() < currentdate.getDate()) {
        document.getElementById("forstartdate").innerHTML = "Select future date";
        return false;
    }
    if (selecteddate.getDate() == currentdate.getDate()) {
        document.getElementById("forstartdate").innerHTML = "Cannot Book Service on Same day";
        return false;
    }

    selecteddate2.setHours(selecteddate2.getHours() + totalHour);
    if (selecteddate.getDate() == selecteddate2.getDate()) {
        var hrs = selecteddate2.getHours();
        if (hrs > 21) {
            document.getElementById("forstartdate").innerHTML = "Could not completed the service request, because service booking request is must be completed within 21:00 time";
            return false;
        }
        else {
            return true;
        }

    }
    else {
        document.getElementById("forstartdate").innerHTML = "Could not completed the service request, because service booking request is must be completed within 21:00 time";
        return false;
    }
}

function rescheduleDone(response) {
    $('#customer-Dashboard').html(response);
    window.location.reload();

}

function tabTwoSuccess(response) {
    $("#schedule-service-tab").removeClass("active");
    $("#schedule-service").removeClass("active");
    $("#schedule-service").addClass("fade");
    $("#service-detials").addClass("active");
    $("#service-detials").removeClass("fade");
    $("#service-detials-tab").addClass("active");
    $("#service-detials-tab").addClass("activen");
    $("#service-detials-tab").prop("disabled", false);
    $("#service-detials").html(response);


    $('#service-detials form').data('validator', null);
    $.validator.unobtrusive.parse('#service-detials form');

   

}

function tabThreeSuccess() {
    $("#service-detials-tab").removeClass("active");
    $("#service-detials").removeClass("active");
    $("#service-detials").addClass("fade");
    $("#service-payment").addClass("active");
    $("#service-payment").removeClass("fade");
    $("service-payment-tab").addClass("active");
    $("service-payment-tab").addClass("activen");
    $("service-payment-tab").prop("disabled", false);

    $('#service-payment form').data('validator', null);
    $.validator.unobtrusive.parse('#service-payment form');

}

function StartDateFunction() {

    var input = document.getElementById("service-date");
    var div = document.getElementById("forStartDate");

    var date = input.value;
    var dataArray = date.split("-");
    var year = dataArray[0];
    var month = dataArray[1];
    var day = dataArray[2];

    var newdate = day + "/" + month + "/" + year;

    if (input.value != "") {

        div.innerHTML = newdate;
    }

}

function StartTimeFunction() {
    var input = document.getElementById("select-service-time");
    var div = document.getElementById("ForStartTime");
    div.innerHTML = input.value;


}

function HourFunction() {
    var input = document.getElementById("select-service-hour");
    var div = document.getElementById("forHour");
    div.innerHTML = input.value;
    printTotalPayment(true);
}

function Etc1Change() {
    var cb = document.getElementById("inside-cabinet");
    if (cb.checked) {
        $("#Etc1").removeClass("d-none");
        $("#Etc-1").removeClass("d-none");
        printTotalPayment(false, 1, 1);
    }
    else {
        $("#Etc1").addClass("d-none");
        $("#Etc-1").addClass("d-none");
        printTotalPayment(false, 1, 0);
    }
}

function Etc2Change() {
    var cb = document.getElementById("Inside-fridge");
    if (cb.checked) {
        $("#Etc2").removeClass("d-none");
        $("#Etc-2").removeClass("d-none");
        printTotalPayment(false, 2, 1);
    }
    else {
        $("#Etc2").addClass("d-none");
        $("#Etc-2").addClass("d-none");
        printTotalPayment(false, 2, 0);
    }
}

function Etc3Change() {
    var cb = document.getElementById("Inside-oven");
    if (cb.checked) {
        $("#Etc3").removeClass("d-none");
        $("#Etc-3").removeClass("d-none");
        printTotalPayment(false, 3, 1);
    }
    else {
        $("#Etc3").addClass("d-none");
        $("#Etc-3").addClass("d-none");
        printTotalPayment(false, 3, 0);
    }
}

function Etc4Change() {
    var cb = document.getElementById("Laundry-wash");
    if (cb.checked) {
        $("#Etc4").removeClass("d-none");
        $("#Etc-4").removeClass("d-none");
        printTotalPayment(false, 4, 1);
    }
    else {
        $("#Etc4").addClass("d-none");
        $("#Etc-4").addClass("d-none");
        printTotalPayment(false, 4, 0);
    }
}

function Etc5Change() {
    var cb = document.getElementById("Interior-windows");
    if (cb.checked) {
        $("#Etc5").removeClass("d-none");
        $("#Etc-5").removeClass("d-none");
        printTotalPayment(false, 5, 1);
    }
    else {
        $("#Etc5").addClass("d-none");
        $("#Etc-5").addClass("d-none");
        printTotalPayment(false, 5, 0);
    }
}

function addone(i) {
    
    if (i == 0)
        document.getElementById("foraddLine1").innerHTML = "";
    else if (i == 1)
        document.getElementById("foraddLine2").innerHTML = "";
    else if (i == 2)
        document.getElementById("formobile").innerHTML = "";

}

function validateNewAddress() {
    var addressline1 = document.getElementById("addLine1").value;
    var addressline2 = document.getElementById("addLine2").value;
    var mobile = document.getElementById("mobile").value;

    if (addressline1 == "") {
        document.getElementById("foraddLine1").innerHTML = "This is required filed";
        return false;
    }
    if (!/^[A-Za-z,0-9 ]+$/.test(addressline1)) {
        document.getElementById("foraddLine1").innerHTML = "Invalid street Name";
        return false;
    }
    if (addressline1.length < 3) {
        document.getElementById("foraddLine1").innerHTML = "must be more than 3 character";
        return false;
    }
    if (addressline1.length > 70) {
        document.getElementById("foraddLine1").innerHTML = "must be less than 70 character";
        return false;
    }

    if (addressline2 == "") {
        document.getElementById("foraddLine2").innerHTML = "This is required filed";
        return false;
    }
    if (!/^[0-9]+$/.test(addressline2)) {
        document.getElementById("foraddLine2").innerHTML = "Only Numbers";
        return false;
    }
    if (addressline2.length > 70) {
        document.getElementById("foraddLine2").innerHTML = "must be less than 70 character";
        return false;
    }
    if (mobile == "") {
        document.getElementById("formobile").innerHTML = "This is required filed";
        return false;
    }
    if (!/^[1-9]+$/.test(mobile)) {
        document.getElementById("formobile").innerHTML = "Only Numbers";
        return false;
    }
    if (mobile.length != 10) {
        document.getElementById("formobile").innerHTML = "Must be 10 digit";
        return false;
    }


    return true;
}

function showAddAddressPartial() {
    event.preventDefault();
    $("#holdAddAddressForm").load("/BookService/AddAddressForm");
    $("#AddAddressButton").addClass("d-none"); 
}



function hideAddAddressPartial() {
    event.preventDefault();
    document.getElementById("holdAddAddressForm").innerHTML = "";
    $("#AddAddressButton").removeClass("d-none");
}

function showDetails(sr) {

    var id = sr.getAttribute("data-id");
    var from = sr.getAttribute("data-from");

    $.ajax({
        type: "GET",
        url: "/Customer/GetServiceDetails?serviceRequestId=" + id,
        success: function (response) {

            $("#modalHolder").html(response);
            if (from == "row") {
                $("#serviceDetails").modal("show");

            }
            else if (from == "button") {

                $("#rescheduleService").modal("show");
            }
            else if (from == "cancel") {
                $("#cancelService").modal("show");

            }
        }
    });
}

function fav(btn) {
    var id = btn.getAttribute("data-id");

    $.ajax({
        type: "GET",
        url: "/Customer/Fav?Id=" + id,
        success: function (response) {

            $('#customer-fav-pro').html(response);
            $('.tab-content div').removeClass("show");
            $('.tab-content div').removeClass("active");
            $('#customer-fav-pro').addClass("show");
            $('#customer-fav-pro').addClass("active")
        }
    });
}

function unfav(btn) {
    var id = btn.getAttribute("data-id");

    $.ajax({
        type: "GET",
        url: "/Customer/UnFav?Id=" + id,
        success: function (response) {

            $('#customer-fav-pro').html(response);
            $('.tab-content div').removeClass("show");
            $('.tab-content div').removeClass("active");
            $('#customer-fav-pro').addClass("show");
            $('#customer-fav-pro').addClass("active")
        }
    });
}

function block(btn) {
    var id = btn.getAttribute("data-id");

    $.ajax({
        type: "GET",
        url: "/Customer/Block?Id=" + id,
        success: function (response) {

            $('#customer-fav-pro').html(response);
            $('.tab-content div').removeClass("show");
            $('.tab-content div').removeClass("active");
            $('#customer-fav-pro').addClass("show");
            $('#customer-fav-pro').addClass("active")
        }
    });
}

function unblock(btn) {
    var id = btn.getAttribute("data-id");

    $.ajax({
        type: "GET",
        url: "/Customer/UnBlock?Id=" + id,
        success: function (response) {

            $('#customer-fav-pro').html(response);
            $('.tab-content div').removeClass("show");
            $('.tab-content div').removeClass("active");
            $('#customer-fav-pro').addClass("show");
            $('#customer-fav-pro').addClass("active")
        }
    });
}


function TRate(r) {
    $('#TRatting').val(r);
    for (var i = 1; i <= r; i++) {
        $('#TRate' + i).attr("class", "fa");
        $('#TRate' + i).addClass("fa-star");
        $('#TRate' + i).addClass("yellow");
    }

    for (var i = r + 1; i <= 5; i++) {
        $('#TRate' + i).attr("class", "fa");
        $('#TRate' + i).addClass("fa-star");
        $('#TRate' + i).addClass('grey');
    }

}

function TRateOver(r) {
    for (var i = 1; i <= r; i++) {
        $('#TRate' + i).addClass("yellow");
    }
}

function TRateOut(r) {
    for (var i = 1; i <= r; i++) {
        $('#TRate' + i).removeClass('yellow');
    }
}

function TRateSelected() {
    var ratting = $('#TRatting').val();
    for (var i = 1; i <= ratting; i++) {
        $('#TRate' + i).addClass("yellow");
    }

}

function FRate(r) {

    $('#FRatting').val(r);
    for (var i = 1; i <= r; i++) {
        $('#FRate' + i).attr("class", "fa");
        $('#FRate' + i).addClass("fa-star");
        $('#FRate' + i).addClass("yellow");
    }

    for (var i = r + 1; i <= 5; i++) {
        $('#FRate' + i).attr("class", "fa");
        $('#FRate' + i).addClass("fa-star");
        $('#FRate' + i).addClass('grey');
    }

}

function FRateOver(r) {
    for (var i = 1; i <= r; i++) {
        $('#FRate' + i).addClass("yellow");
    }
}

function FRateOut(r) {
    for (var i = 1; i <= r; i++) {
        $('#FRate' + i).removeClass('yellow');
    }
}

function FRateSelected() {
    var ratting = $('#FRatting').val();
    for (var i = 1; i <= ratting; i++) {
        $('#FRate' + i).addClass("yellow");
    }

}

function QRate(r) {

    $('#QRatting').val(r);
    for (var i = 1; i <= r; i++) {
        $('#QRate' + i).attr("class", "fa");
        $('#QRate' + i).addClass("fa-star");
        $('#QRate' + i).addClass("yellow");
    }

    for (var i = r + 1; i <= 5; i++) {
        $('#QRate' + i).attr("class", "fa");
        $('#QRate' + i).addClass("fa-star");
        $('#QRate' + i).addClass('grey');
    }

}

function QRateOver(r) {
    for (var i = 1; i <= r; i++) {
        $('#QRate' + i).addClass("yellow");
    }
}

function QRateOut(r) {
    for (var i = 1; i <= r; i++) {
        $('#QRate' + i).removeClass('yellow');
    }
}

function QRateSelected() {
    var ratting = $('#QRatting').val();
    for (var i = 1; i <= ratting; i++) {
        $('#QRate' + i).addClass("yellow");
    }

}

function showRate(sr) {

    var id = sr.getAttribute("data-id");
    var from = sr.getAttribute("data-from");

    $.ajax({
        type: "GET",
        url: "/Customer/GetRattingDetails?serviceRequestId=" + id,
        success: function (response) {

            $("#modalHolder").html(response);
            if (from == "row") {
                $("#serviceDetails").modal("show");

            }
            else if (from == "rate") {

                $("#RateSp").modal("show");
                TRateSelected();
                FRateSelected();
                QRateSelected();
            }

        }
    });
}

function validateRating() {
    var Rate = $("#TRatting").val();
    if (Rate == 0) {
        $("#forOnTime").html("Please Rate for On Time Arriaval");
        return false;
    }
    Rate = $("#FRatting").val();
    if (Rate == 0) {
        $("#forFriendly").html("Please Rate for Friendly");
        return false;
    }
    Rate = $("#QRatting").val();
    if (Rate == 0) {
        $("#forQuality").html("Please Rate for Quality of Service");
        return false;
    }
    return true;
}

function ratingSuccess(response) {
    debugger;
    $('#customer-Service-History').html(response);
    $('#RateSp').modal("toggle");
    $("#logout-Modal").modal("toggle");
    $("#text-message").html("Successfully Rated the Service Provider");

}

function successDetailsUpdate(response) {

    $("#content-mydetails").html(response);
    $("#SuccessUpdateDetails").removeClass("d-none");
    $("#SuccessUpdateDetails").html("Data Successfully Updated");
    setTimeout(
        function () {
            $("#SuccessUpdateDetails").addClass("d-none");
        }, 3000);

}

function showErrorDetails(jqXHR) {

    var data = JSON.parse(jqXHR.responseText);
    if (data.success == false) {
        if (data.errorFor == "dates") {
            $("#forDates").html(data.messege);
        }
    }
}


function successUpdatePassword(response) {
    $("#content-mydetails").html(response);
    $("#SuccessUpdatePassword").removeClass("d-none");
    $("#SuccessUpdatePassword").html("Data Successfully Updated");
    setTimeout(
        function () {
            $("#SuccessUpdatePassword").addClass("d-none");
        }, 3000);
}

function passwordFailure(data) {
    var val = JSON.parse(data.responseText);
    $("#SuccessUpdatePassword").removeClass("d-none");
    $("#SuccessUpdatePassword").removeClass("alert-success");
    $("#SuccessUpdatePassword").addClass("alert-danger");
    $("#SuccessUpdatePassword").html(val.messege);
    setTimeout(
        function () {
            $("#SuccessUpdatePassword").addClass("d-none");
        }, 3000);
}

function showPartial(data) {

    var id = data.getAttribute("data-id");
    var from = data.getAttribute("data-from");
    if (from == "new") {
        $.ajax({
            type: "GET",
            url: "/Customer/LoadAddressPartialNew",
            success: function (response) {
                $("#holdpartialForAddress").html(response);
                $('#holdpartialForAddress form').data('validator', null);
                $.validator.unobtrusive.parse('#holdpartialForAddress form');

                $("#editAddress").modal("show");

            }

        });
    }
    else {

        $.ajax({
            type: "GET",
            url: "/Customer/LoadAddressPartial?addressId=" + id,
            success: function (response) {
                $("#holdpartialForAddress").html(response);
                $('#holdpartialForAddress form').data('validator', null);
                $.validator.unobtrusive.parse('#holdpartialForAddress form');
                if (from == "old") {
                    $("#editAddress").modal("show");
                }
                else if (from == "del") {
                    $("#deleteAddress").modal("show");
                }
            }

        });
    }
}

function getCity(data) {

    var postalcode = data.value;
    $("#forWrongZipcode").html("");
    $("#CitySelect").html("");

    if (/^[1-9][0-9]{5}$/.test(postalcode)) {
        if (postalcode.length == 6) {
            $.ajax({
                type: "GET",
                url: "/Customer/GetCity?postalCode=" + postalcode,
                success: function (response) {

                    if (response == "empty") {
                        $("#forWrongZipcode").html("No City with this Postal Code")
                        $("#CitySelect").html("");
                    }
                    else {
                        $("#CitySelect").html('<option value="' + response + '" selected>' + response + '</option>');
                    }
                }
            });
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}

function newAddressSuccess(data) {
    $("#editAddress").modal("toggle");
    $("#content-mydetails").html(data);
    $("#SuccessAddressDetails").removeClass("d-none");
    $("#SuccessAddressDetails").html("Successfully Added Address");
    setTimeout(
        function () {
            $("#SuccessAddressDetails").addClass("d-none");
        }, 3000);
}

function newAddressSuccessTwo(data) {
    $("#editAddress").modal("toggle");
    $("#content-mydetails").html(data);
    $("#SuccessAddressDetails").removeClass("d-none");
    $("#SuccessAddressDetails").html("Successfully Updated Address");
    setTimeout(
        function () {
            $("#SuccessAddressDetails").addClass("d-none");
        }, 3000);
}

function DeleteAddressSuccess(data) {
    $("#deleteAddress").modal("toggle");
    $("#content-mydetails").html(data);
    $("#SuccessAddressDetails").removeClass("d-none");
    $("#SuccessAddressDetails").html("Successfully Deleted Address");
    setTimeout(
        function () {
            $("#SuccessAddressDetails").addClass("d-none");
        }, 3000);
}


function callPageSize(data, what) {

    var no = data.value;
    if (what == 1) {
        $.ajax({
            type: "GET",
            url: "/Customer/GetDashboard?pageSize=" + no,
            success: function (response) {

                $("#customer-Dashboard").html(response);
            }
        });
    }
    else {
        $.ajax({
            type: "GET",
            url: "/Customer/GetCustomerServiceHistory?pageSize=" + no,
            success: function (response) {

                $("#customer-Service-History").html(response);
            }
        });
    }

}

function SortSuccess(data) {

    $("#customer-Service-History").html(data);
}

function SortSuccess2(data) {

    $("#customer-Dashboard").html(data);
}







