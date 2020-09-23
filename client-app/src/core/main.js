
//نمایش پیغام خطا
var FailedAlert = function (msg) {
    swal({
        title: "خطا!",
        text: msg,
        type: "error",
        showCancelButton: false,
        confirmButtonClass: "btn btn-danger",
        confirmButtonText: "باشه",
        buttonsStyling: false
    });
}


var FailedAlert2 = function (msg,title, confirmButtonText) {
    swal({
        title: `${title}!`,
        text: msg,
        type: "error",
        showCancelButton: false,
        confirmButtonClass: "btn btn-danger",
        confirmButtonText: confirmButtonText,
        buttonsStyling: false
    });
}



//نمایش پیغام موفقیت آمیز
var SuccessAlert = function (msg) {
    swal({
        title: "انجام شد",
        text: msg,
        type: "success",
        showCancelButton: false,
        confirmButtonClass: "btn btn-success",
        confirmButtonText: "باشه",
        buttonsStyling: false
    });
}

var SuccessAlert2 = function (msg, title, confirmButtonText) {
    swal({
        title: title,
        text: msg,
        type: "success",
        showCancelButton: false,
        confirmButtonClass: "btn btn-success",
        confirmButtonText: confirmButtonText,
        buttonsStyling: false
    });
}


var toShortString = function (text, count)  {
    if (!text) return "";
    if (text.length >= count) return text.substring(0, count) + "[...]";
    else return text;
};




var getPriceFormat = function (price)  {
    if (!price)
        return null;
    price += '';
    price = price.replace(',', '');
    let x = price.split('.');
    let y = x[0];
    let z = x.length > 1 ? '.' + x[1] : '';
    let rgx = /(\d+)(\d{3})/;
    while (rgx.test(y))
        y = y.replace(rgx, '$1' + ',' + '$2');
    return y + z;
}




/*   اینپوتاها نتوانن اسپیس وارد کنند برای حرف اولشان   */
// <input  oninput="validate(this)" >
function validate(input) {
    if (/^\s/.test(input.value))
        input.value = '';
}




//get query string value 
function getQueryString() {
    var query_string = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (typeof query_string[pair[0]] === "undefined") {
            query_string[pair[0]] = pair[1];
        } else if (typeof query_string[pair[0]] === "string") {
            var arr = [query_string[pair[0]], pair[1]];
            query_string[pair[0]] = arr;
        } else {
            query_string[pair[0]].push(pair[1]);
        }
    }
    return query_string;
}