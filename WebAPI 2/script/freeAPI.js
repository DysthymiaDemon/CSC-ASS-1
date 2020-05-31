var apiBaseURL = 'http://api.worldweatheronline.com/free/v1/';
var apiKey = '115ce3fb547542a0b4294516201705';


var _PremiumApiBaseURL = 'http://api.worldweatheronline.com/premium/v1/';
/*
    Please change the PremiumAPIKey to your own. 
    These keys have been provided for testing only.
    If you don't have one, then register now: http://developer.worldweatheronline.com/member/register    
*/
var _PremiumApiKey = 'w9ve379xdu8etugm7e2ftxd6';
// -------------------------------------------

function JSONP_LocalWeather(input) {
    var url = apiBaseURL + 'weather.ashx?q=' + input.query + '&format=' + input.format + '&extra=' + input.extra + '&num_of_days=' + input.num_of_days + '&date=' + input.date + '&fx=' + input.fx + '&cc=' + input.cc + '&includelocation=' + input.includelocation + '&show_comments=' + input.show_comments + '&key=' + apiKey;

    jsonP(url, input.callback);
}


// -------------------------------------------

// Helper Method
function jsonP(url, callback) {
    $.ajax({
        type: 'GET',
        url: url,
        async: false,
        contentType: "application/json",
        jsonpCallback: callback,
        dataType: 'jsonp',
        success: function (json) {
            console.dir('success');
        },
        error: function (e) {
            console.log(e.message);
        }
    });
}


