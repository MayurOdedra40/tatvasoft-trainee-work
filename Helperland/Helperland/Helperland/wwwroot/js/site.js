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
function tabTwoSuccess() {
    $("#schedule-service-tab").removeClass("active");
    $("#schedule-service").removeClass("active");
    $("#schedule-service").addClass("fade");
    $("#service-detials").addClass("active");
    $("#service-detials").removeClass("fade");
    $("#service-detials-tab").addClass("active");
    $("#service-detials-tab").addClass("activen");
    $("#service-detials-tab").prop("disabled", false);

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

function ChangeHourAdd() {
    debugger;


}

function ChangeHourSub() {
    debugger;


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



