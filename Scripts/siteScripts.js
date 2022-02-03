$(document).ready(function () {
    BindControls();
});
function BindControls() {
    $("#searchMovieTitle").autocomplete({
        source: function (request, response) {

            var val = request.term;

            $.ajax({
                url: "https://www.omdbapi.com/?apikey=c279cfec&s=" + val,
                type: "GET",
                success: function (data) {
                    response($.map(data, function (item) {



                        const obj = JSON.parse(JSON.stringify(item));
                        return obj[0].Title;

                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1   // MINIMUM 1 CHARACTER TO START WITH.
    });
}