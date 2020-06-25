$('#search').keyup(function () {
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
});

const spinner = document.getElementById("spinner");

function showSpinner() {
    spinner.className = "show";
}

function hideSpinner() {
        spinner.className = spinner.className.replace("show", "");
}
