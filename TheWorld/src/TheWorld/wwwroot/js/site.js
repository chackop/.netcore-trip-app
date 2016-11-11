(function () {
    
    //var el = $("#usern");
    //el.text("chax");

    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.style = "backgroundcolor: gray";
    //});

    //main.on("mouseleave", function () {
    //    main.style = "";
    //});

    //var menuitems = $("ul.menu li a");
    //menuitems.on("click", function () {
    //    var me = $(this);
    //    alert(me.text());
    //});

    var $sidebarap = $('#sidebar', '#wrapper');
    var $icon = $('#sidebarToggle i.fa');
    $sidebarap.on("click", function () {
        $sidebarap.toggleClass("hide-sidebar");
        if ($sidebarap.hasClass("hide-sidebar")) {
            //$(this).text("Show Sidebar");
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });
})();


