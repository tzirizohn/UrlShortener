$(() => {

    $("#shorten-button").on('click', function () {
        const longurl = $("#longurl").val();
        const id = $("#id").val();
        $.post('/home/addurl', { longurl}, function (shorturl) {  
            $("#shorturl").val("http://localhost:62371/" + shorturl);       
        });
    });             
});