$('#search').keyup(function () {
    //get data from json file
    var urlForJson = [
        {
            "Id": 0,
            "Name": "Barot Bellingham",
            "ShortName": "Barot_Bellingham",
            "Reknown": "Royal Academy of Painting and Sculpture",
            "Bio": "Barot has just finished his final year at The Royal Academy of Painting and Sculpture, where he excelled in glass etching paintings and portraiture. Hailed as one of the most diverse artists of his generation, Barot is equally as skilled with watercolors as he is with oils, and is just as well-balanced in different subject areas. Barot's collection entitled \"The Un-Collection\" will adorn the walls of Gilbert Hall, depicting his range of skills and sensibilities - all of them, uniquely Barot, yet undeniably different"
        },
        {
            "Id": 1,
            "Name": "Jonathan G. Ferrar II",
            "ShortName": "Jonathan_Ferrar",
            "Reknown": "Artist to Watch in 2012",
            "Bio": "The Artist to Watch in 2012 by the London Review, Johnathan has already sold one of the highest priced-commissions paid to an art student, ever on record. The piece, entitled Gratitude Resort, a work in oil and mixed media, was sold for $750,000 and Jonathan donated all the proceeds to Art for Peace, an organization that provides college art scholarships for creative children in developing nations"
        },
        {
            "Id": 2,
            "Name": "Hillary Hewitt Goldwynn-Post",
            "ShortName": "Hillary_Goldwynn",
            "Reknown": "New York University",
            "Bio": "Hillary is a sophomore art sculpture student at New York University, and has already won all the major international prizes for new sculptors, including the Divinity Circle, the International Sculptor's Medal, and the Academy of Paris Award. Hillary's CAC exhibit features 25 abstract watercolor paintings that contain only water images including waves, deep sea, and river."
        },
        {
            "Id": 3,
            "Name": "Hassum Harrod",
            "ShortName": "Hassum_Harrod",
            "Reknown": "Art College in New Dehli",
            "Bio": "The Art College in New Dehli has sponsored Hassum on scholarship for his entire undergraduate career at the university, seeing great promise in his contemporary paintings of landscapes - that use equal parts muted and vibrant tones, and are almost a contradiction in art. Hassum will be speaking on \"The use and absence of color in modern art\" during Thursday's agenda."
        }
    ];


    //get data from Restful web Service in development environment
    //var urlForJson = "http://localhost:9000/api/talents";

    //get data from Restful web Service in production environment
    //var urlForJson= "http://csc123.azurewebsites.net/api/talents";

    //Url for the Cloud image hosting
    var urlForCloudImage = "https://res.cloudinary.com/myridianstar/image/upload/v1562897665/samples/cloudinary-group.jpg";

    var searchField = $('#search').val();
    var myExp = new RegExp(searchField, "i");
    $.getJSON(urlForJson, function (data) {
        var output = '<ul class="searchresults">';
        $.each(data, function (key, val) {
            //for debug
            console.log(data);
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
        });
        output += '</ul>';
        $('#update').html(output);
    }); //get JSON
});
