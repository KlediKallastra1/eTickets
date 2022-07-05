
var countDownDate = new Date("Jul 9, 2022 15:37:25").getTime();
document.getElementById("shopnowhotdeal").innerHTML = "SHOP NOW";

var x = setInterval(function() {

    var now = new Date().getTime();

    var distance = countDownDate - now;

    var days = Math.floor(distance / (1000 * 60 * 60 * 24));
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    document.getElementById("days").innerHTML = days;
    document.getElementById("hours").innerHTML = hours;
    document.getElementById("mins").innerHTML = minutes;
    document.getElementById("secs").innerHTML = seconds;

    if (distance < 0) {
        clearInterval(x);
        document.getElementById("days").innerHTML = "N/A";
        document.getElementById("hours").innerHTML = "N/A";
        document.getElementById("mins").innerHTML = "N/A";
        document.getElementById("secs").innerHTML = "N/A";
        document.getElementById("shopnowhotdeal").innerHTML = "EXPIRED";
        document.getElementById("shopnowhotdeal").disabled = true;
    }
}, 1000);