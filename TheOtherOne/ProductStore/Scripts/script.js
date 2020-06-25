$('#search').keyup(function () {
    //get data from Restful web Service in development environment
    //var urlForJson = "http://localhost:9000/api/talents";

    //get data from Restful web Service in production environment
    //var urlForJson= "http://csc123.azurewebsites.net/api/talents";

    //Url for the Cloud image hosting
    var urlForCloudImage = "https://res.cloudinary.com/myridianstar/image/upload/v1593078383/foreigntalent/";

    var searchField = $('#search').val();
    var myExp = new RegExp(searchField, "i");
    var output = '<ul class="searchresults">';
    showSpinner();

    try {
        $.ajax({
            type: 'GET',
            url: '/api/v1/talents'
        }).done(function (data) {
            $.each(data, function (key, val) {
                hideSpinner();
                if ((val.Name.search(myExp) != -1) ||
                    (val.Bio.search(myExp) != -1)) {
                    output += '<li>';
                    output += '<h2>' + val.Name + '</h2>';
                    //get the absolute path for local image
                    //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                    //get the image from cloud hosting
                    output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                    output += '<p>' + val.Bio + '</p>';
                    output += '</li>';

                }
            })
            output += '</ul>';
            $('#update').html(output);

        }).fail(function (error) {
            output += "Error"
            output += '</ul>';
            $('#update').html(output);
            hideSpinner();
        });
    } catch {
        output += "Error"
        output += '</ul>';
        $('#update').html(output);
        hideSpinner();
    }

    // $.getJSON(urlForJson, function (data) {
    //     var output = '<ul class="searchresults">';
    //     $.each(data, function (key, val) {
    //         //for debug
    //         console.log(data);
    //         if ((val.Name.search(myExp) != -1) ||
    //(val.Bio.search(myExp) != -1)) {
    //             output += '<li>';
    //             output += '<h2>' + val.Name + '</h2>';
    //             //get the absolute path for local image
    //             //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

    //             //get the image from cloud hosting
    //             output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
    //             output += '<p>' + val.Bio + '</p>';
    //             output += '</li>';
    //         }
    //     });
});

const spinner = document.getElementById("spinner");

function showSpinner() {
    spinner.className = "show";
    //setTimeout(() => {
    //    spinner.className = spinner.className.replace("show", "");
    //}, 2000);
}

function hideSpinner() {
    //spinner.className = "show";
    setTimeout(() => {
        spinner.className = spinner.className.replace("show", "");
    }, 2000);
}
