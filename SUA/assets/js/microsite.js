


var micrositeString = "[{ id: 1,  name: &quot;educatina&quot; },{ id: 2,  name: &quot;up&quot; },{ id: 3,  name: &quot;meli&quot; },{ id: 5,  name: &quot;rivadavia&quot; },{ id: 7,  name: &quot;mvl&quot; },{ id: 9,  name: &quot;tigre&quot; },{ id: 12,  name: &quot;demo&quot; },{ id: 17,  name: &quot;ingreso-unlam&quot; },{ id: 19,  name: &quot;ingreso-utn&quot; },{ id: 27,  name: &quot;utdt&quot; },{ id: 43,  name: &quot;fcm&quot; },{ id: 45,  name: &quot;paula-albarracin&quot; },{ id: 46,  name: &quot;norbridge&quot; },{ id: 48,  name: &quot;tareasenespanol&quot; },{ id: 49,  name: &quot;historiadeltodo&quot; },{ id: 52,  name: &quot;cablevision.old&quot; },{ id: 53,  name: &quot;mpl&quot; },{ id: 58,  name: &quot;aprendepilar&quot; },{ id: 59,  name: &quot;cablevision&quot; },{ id: 60,  name: &quot;cablevision2&quot; },{ id: 61,  name: &quot;villamaria&quot; },{ id: 62,  name: &quot;vm&quot; },{ id: 64,  name: &quot;tdf&quot; },{ id: 65,  name: &quot;jujuy&quot; },{ id: 66,  name: &quot;villamaria1&quot; },{ id: 67,  name: &quot;villamaria-test4&quot; },{ id: 70,  name: &quot;villamaria2&quot; },{ id: 71,  name: &quot;villamaria3&quot; },{ id: 72,  name: &quot;villamaria4&quot; },{ id: 73,  name: &quot;villamaria5&quot; },{ id: 74,  name: &quot;template&quot; },{ id: 75,  name: &quot;honduras&quot; },{ id: 77,  name: &quot;cbc&quot; },{ id: 78,  name: &quot;&quot; },{ id: 79,  name: &quot;casares&quot; },{ id: 81,  name: &quot;jalisco&quot; },{ id: 83,  name: &quot;nuc&quot; },{ id: 84,  name: &quot;nacion&quot; },{ id: 85,  name: &quot;inglesnacion&quot; },{ id: 86,  name: &quot;primariasnacion&quot; },{ id: 87,  name: &quot;matematicanacion&quot; },{ id: 88,  name: &quot;tutorial&quot; },{ id: 89,  name: &quot;muybien10&quot; },{ id: 91,  name: &quot;inet&quot; },{ id: 92,  name: &quot;unra-test&quot; },{ id: 101,  name: &quot;unra-demo&quot; },{ id: 102,  name: &quot;rafaela-demo&quot; },{ id: 103,  name: &quot;YPF&quot; },{ id: 104,  name: &quot;perueduca&quot; },{ id: 105,  name: &quot;malvinas&quot; },{ id: 106,  name: &quot;munirivadavia&quot; },{ id: 107,  name: &quot;cencosud&quot; },{ id: 108,  name: &quot;hp&quot; },{ id: 109,  name: &quot;edutec&quot; },{ id: 110,  name: &quot;2222&quot; },{ id: 111,  name: &quot;axion&quot; },{ id: 112,  name: &quot;level3&quot; },{ id: 113,  name: &quot;demo-formacion&quot; },{ id: 114,  name: &quot;aula365-demo&quot; },{ id: 123,  name: &quot;Aula365&quot; },{ id: 124,  name: &quot;Aula365-iberica&quot; },{ id: 126,  name: &quot;demonacion2018&quot; },{ id: 127,  name: &quot;alianzalima&quot; },{ id: 128,  name: &quot;champagnat&quot; },{ id: 129,  name: &quot;fpf&quot; },{ id: 131,  name: &quot;monterrico&quot; },{ id: 132,  name: &quot;bruno_test&quot; },{ id: 133,  name: &quot;fakeA&quot; },{ id: 134,  name: &quot;fakeB&quot; },]"
micrositeString = micrositeString.replace(/&quot;/g, "'")
var microsites = JSON.parse(JSON.stringify(eval('(' + micrositeString + ')')))
getMicrositeNameFromId = function (id) {
    for (var i = 0; i < microsites.length; i++) {
        if (parseInt(microsites[i].id) == parseInt(id)) return microsites[i].name
    }
    return 0
}
getMicrositeIdFromKey = function (key) {
    for (var i = 0; i < microsites.length; i++) {
        if (microsites[i].name == key) return parseInt(microsites[i].id)
    }
    return ""
}

function replaceIdForKey() {
    $('.replaceCode').each(function () {
        id = this.innerHTML
        $(this).attr("value", id)
        this.innerHTML = getMicrositeNameFromId(parseInt(id))
    })
}
replaceIdForKey();



$(document).ready(function () {
    /*
    $("#videoThumbnail").hide();
    $("#videoUrl").on("blur", getYoutubeIdAndShowImage);

    var titulo = $(document).find("title").text();
    if (titulo == "Back Office - PDF") {
        //$(window).scrollTop(200);
        $("#uploadContent").contents().find("nav").hide();
        $("#uploadContent").contents().find(".maiBox").css("margin-top", "-200px");

        $(".container-fluid").hide();
        $(".navbar navbar-default").hide();
        window.scrollTo(0, document.body.scrollHeight);
    }
    */

});

function getYoutubeIdAndShowImage() {
    var url = $("#videoUrl").val();
    if (url != "" && url.indexOf("v=") > -1) {
        var video_id = $("#videoUrl").val().split("v=")[1].substring(0, 11);
        $("#videoThumbnail").attr("src", "http://img.youtube.com/vi/" + video_id + "/1.jpg");
        $("#videoThumbnail").show();
    }
    else {
        $("#videoThumbnail").hide();
        $("#videoThumbnail").attr("src", "");
    }
}

$(document).on("mouseenter", ".couponcode", function () {
    this.addEventListener('mousemove', function fn(e) {

        if ($(this).find(".coupontooltip").length == 0) {
            _tp = document.createElement("SPAN")
            _tp.setAttribute("class", "coupontooltip")
            _tp.innerHTML = getMicrositeNameFromId(parseInt(this.innerHTML))

            this.appendChild(_tp)
        }

        var tooltip = e.target.classList.contains("coupontooltip") ?
            e.target :
            e.target.querySelector(':scope .coupontooltip');
        tooltip.style.left =
            e.pageX + tooltip.clientWidth + 10 <
                document.body.clientWidth ?
                e.pageX + 10 + 'px' :
                document.body.clientWidth + 5 - tooltip.clientWidth + 'px';
        tooltip.style.top =
            e.screenY - 300 + 'px'
    });
})