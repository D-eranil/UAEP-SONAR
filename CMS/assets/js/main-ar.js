var scroll,
    menuButton = $('.menu-open'),
    $homeSlider = $('.mainBanner'),
    $sliderAnimationSpeed = 7000,
    circleInstances = {},
    $status = $('.current'),
    $slidesControls = $('.slidesCount'),
    $happeningSlider = $(".happening-slider"),
    $venuedetailSlider = $(".venue-detail-slider"),
    $activitySlider = $(".activity-slider"),
    $wisdomTxtSlider = $(".wisdom-txt-slider"),
    // $wisdomImgSlider = $(".wisdom-img-slider"),

    $wisdomImgSlider = $(".page-view"),
    $wisdomSlider = $(".wisdom-slider"),
    $WisdomIconSlider = $(".wisdom-icon-slider"),
    $missionSlider = $(".mission-slider"),

    //$primarySchoolForm = $("#primary-school-form"),
    // $paymentMethodForm = $("#payment-method-form"),
    // $feedbackForm = $("#feedback-form"),
    $indForm = $("#ind-form"),

    $factsSlider = $(".facts-slider"),
    $guinnessSlider = $(".guinness-slider"),
    $partnersSlider = $(".partners-slider"),
    $PressListSlider = $(".press-list-slider"),
    $PressThumbSlider = $(".press-thumb-slider"),
    $MediaGallerySlider = $(".media-gallery-slider"),
    $numbercounter = $(".number-counter"),
    $SchTripSlider = $(".sch-trip-slider"),
    $partnersLogoSlider = $(".partners-logo-slider"),
    $downloadSlider = $(".download-slider"),
    emailRegex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
    //letters = /^[a-zA-Z ]*$/,
    letters = /^[\u0621-\u064A ]+$/,
    //letters = /^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$/,
    timeRegex = /^([0 - 1] ? [0 - 9] | 2[0 - 3]): [0 - 5][0 - 9]$/,
    // phone_numberRegex = /^([\+][0-9]{1,3}([ \.\-])?)?([\(][0-9]{1,6}[\)])?([0-9 ]{1,45})(([\:]{1,11})?[0-9]{1,4}?)$/;
    phone_numberRegex = /^[+]*[(]{0,1}[\u0030-\u0039\u0660-\u0669]{1,3}[)]{0,1}[-\s\./\u0030-\u0039\u0660-\u0669]*$/;

function animateElements() {
    if ($('.animate').length > 0) {
        $('.animate').bind('inview', function (event, isInView) {
            if (isInView && !$("body").hasClass("dont-animate")) {
                var animate = $(this).attr('data-animation');
                var speedDuration = $(this).attr('data-duration');
                var $t = $(this);
                setTimeout(function () {
                    $t.addClass(animate + ' animated');
                }, speedDuration);
            }
        });
    }

    $('.custom-animate').bind('inview', function (event, isInView) {
        if (isInView) {
            setTimeout(function () {
                $(".special-paper-spirit").addClass("paper-start");
            }, 1200);
        }
    });

}

function downloadSliderInit() {
    if ($downloadSlider.length > 0) {
        if ($(window).width() < 576) {
            $downloadSlider.slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                infinite: true,
                prevArrow: '<a href="javascript:" class="slick-arrow-right"></a>',
                nextArrow: '<a href="javascript:" class="slick-arrow-left"></a>',
                responsive: [
                    {
                        breakpoint: 576,
                        settings: {
                            slidesToShow: 1,
                        }
                    }
                ]
            });
        }
    }
}

function partnersSliderInit() {
    if ($partnersLogoSlider.length > 0) {
        if ($(window).width() < 576) {
            $partnersLogoSlider.slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                infinite: true,
                prevArrow: '<a href="javascript:" class="slick-arrow-right"></a>',
                nextArrow: '<a href="javascript:" class="slick-arrow-left"></a>',
                responsive: [
                    {
                        breakpoint: 576,
                        settings: {
                            slidesToShow: 1,
                        }
                    }
                ]
            });
        }
    }
}

function HomeSliderInit() {
    if ($homeSlider.length > 0) {
        var isdot = false;
        if ($homeSlider.children().length > 1) {
            isdot = true;
        }

        $homeSlider
            .on('init reInit afterChange', function (event, slick, currentSlide, nextSlide) {

                if (slick.slideCount > 1) {
                    $('.total').text('0' + slick.slideCount);
                    var i = (currentSlide ? currentSlide : 0) + 1;
                    $status.text('0' + i);
                    isdot = true;
                }
                if ($("#circle-border-container0").find("svg").length > 0) {
                    $("#circle-border-container0").html("");
                }
                var bar = new ProgressBar.Line('#circle-border-container0', {
                    strokeWidth: 1,
                    easing: 'easeInOut',
                    duration: $sliderAnimationSpeed,
                    color: '#ffffff',
                    trailColor: '#8ea26a',
                    trailWidth: 1,
                    svgStyle: { width: '100%', height: '100%' }
                });
                circleInstances['circle-border-container0'] = bar;
                bar.animate(0);
                circleInstances['circle-border-container0'].animate(1.0);
            })
            .on('beforeChange', function (event, slick, currentSlide, nextSlide) {
                if ($("#circle-border-container" + nextSlide).find("svg").length > 0) {
                    $("#circle-border-container" + nextSlide).html("");
                }
                var bar = new ProgressBar.Line('#circle-border-container' + nextSlide, {
                    strokeWidth: 1,
                    easing: 'easeInOut',
                    duration: $sliderAnimationSpeed,
                    color: '#ffffff',
                    trailColor: '#8ea26a',
                    trailWidth: 1,
                    svgStyle: { width: '100%', height: '100%' }
                });
                circleInstances['circle-border-container' + nextSlide] = bar;
                bar.animate(nextSlide);
                circleInstances['circle-border-container' + nextSlide].animate(1.0);
                $homeSlider.find('.animate').off('inview');
                var currentElem = slick.$slides[nextSlide], $currentSlideElem = $(currentElem);
                var $elem = $currentSlideElem.find('.slide-text').children();
                $($elem).each(function (i, v) {
                    if ($(v).hasClass('animated')) {
                        $(v).removeClass('fadeInUp animated');
                        $(v).removeClass('fadeInLeft animated');
                        $(v).removeClass('fadeInRight animated');
                        $(v).removeClass('fadeInDown animated');
                    }
                    $(v).addClass('animate');
                });
                $currentSlideElem.find('.animate').each(function () {
                    $(this).bind('inview', function (event, isInView) {
                        if (isInView) {
                            var animate = $(this).attr('data-animation');
                            var speedDuration = $(this).attr('data-duration');
                            var $t = $(this);
                            setTimeout(function () {
                                $t.addClass(animate + ' animated');
                            }, speedDuration);
                        }
                    });
                });
                $homeSlider.find('.animate').trigger('inview', [true]);
            })
            .slick({

                autoplay: true,
                rtl: true,
                autoplaySpeed: $sliderAnimationSpeed,
                fade: true,
                infinite: true,
                dots: isdot,
                dotsClass: 'banner-dots',
                appendDots: $slidesControls,
                pauseOnFocus: false,
                swipe: false,
                lazyLoad: 'ondemand',
                customPaging: function (slider, i) {
                    var circleElem = '<a href="javascript:"><span id="circle-border-container' + i + '" class="cirle-border"></span></a>';
                    return circleElem;
                },
                arrows: false,
                pauseOnHover: false,
                responsive: [
                    {
                        breakpoint: 676,
                        settings: {
                            swipe: true
                        }
                    }
                ]
            })

    }
}
$('.back-to-top').fadeOut();

function LocoScroll() {
    if ($("[data-scroll-container]").length && !$("body").hasClass("EditMode")) {
        scroll = new LocomotiveScroll({
            el: document.querySelector('[data-scroll-container]'),
            smooth: true
        });
        setTimeout(function () { window.scrollTo(0, 0); }, 10);

        $("body").on("mouseenter", ".bootstrap-select .dropdown-menu", function () {
            scroll.stop();
        });
        $("body").on("mouseleave", ".bootstrap-select .dropdown-menu", function () {
            scroll.start();
        });

        $("body").on("mouseenter", ".iti__flag-container", function () {
            scroll.stop();
        });
        $("body").on("mouseleave", ".iti__flag-container", function () {
            scroll.start();
        });
    }
    // console.log(!$("body").hasClass("EditMode"));
    if ($('.back-to-top').length > 0) {
        scroll.on('scroll', function (obj) {

            if ((obj.scroll.y) > $(window).height()) {
                $('.back-to-top').fadeIn();

            }
            else {
                $('.back-to-top').fadeOut();
            }

        });
    }
}



/*function scrollBarInit() {
    scroll = Scrollbar.init(
        document.querySelector(".scroll-wrapper")
    );
    scroll.addListener(function (status) {
    });
}*/


function iePopup() {
    setTimeout(function () {
        if ($("html").hasClass("ie")) {
            $('#iePopup').modal('show');
        }
    }, 2000);
}
function getEventsListing() {
    $.ajax({
        type: 'GET',
        url: '/GetAllEvents.aspx',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null && data.ListData.length > 0) {
                //var response = $.parseJSON(data);
                var arrEvents = [];
                $.each(data, function (i, e) {
                    var objEvent = {};
                    objEvent.title = e.Title;
                    objEvent.allday = true;
                    objEvent.start = e.StartDate;
                    objEvent.end = e.EndDate;
                    objEvent.imageurl = e.Image;
                    objEvent.url = e.Url;
                    objEvent.description = "<p>" + e.Description + "</p>";
                    objEvent.backgroundColor = e.Color;
                    arrEvents.push(objEvent);
                });
                //$('#calendar').fullCalendar('getCalendar');
                cal = $('.bootstrapModalFullCalendar').fullCalendar('getCalendar');
                cal.removeEvents();
                cal.addEventSource(arrEvents);
            }
            else {
                isInProgress = true;
            }
        }
    });
}

function faqDropdownSlider() {
    $('.dropdown-slider h5').click(function () {

        $('.dropdown-slider h5').not(this).parent().find('p').slideUp();
        $('.dropdown-slider h5').not(this).parent().find('.icon-vert').css('transform', 'rotate(0deg)');
        $('.dropdown-slider h5').not(this).parent().removeClass('toggle-active');
        $(this).parent().find('p').slideToggle();

        if (!$(this).parent().hasClass('toggle-active')) {
            $(this).parent().find('.icon-vert').css('transform', 'rotate(90deg)');
            $(this).parent().addClass('toggle-active');
        } else {
            $(this).parent().find('.icon-vert').css('transform', 'rotate(0deg)');
            $(this).parent().removeClass('toggle-active');
        }
    })
}

function checkboxesValuesOnRefresh() {
    $("#duraton").val($('.visit-park:checked').val());
    $("#newsleter").val($('.receive-update:checked').val());
    $("#overallexp").val($('.overall:checked').val());
    $("#clean").val($('.cleanliness:checked').val());
    $("#valuemoney").val($('.money-check:checked').val());
    $("#staffprofessional").val($('.staff-check:checked').val());
    $("#diningexp").val($('.dining-check:checked').val());
    $("#parkfacility").val($('.park-check:checked').val());
    $("#operatinghour").val($('.operating-check:checked').val());
    $("#eventsandactivity").val($('.events-check:checked').val());
    $("#recomendation").val($('.recommend-check:checked').val());
}


function SizeInKb() {
    $(".attachmentSize").each(function () {
        var size = $(this).text();
        var sizeInKb = size / 1024;
        var roundSize = Math.round(sizeInKb);
        $(this).text(roundSize + "KB");
    });
}

var path, divId;
function DropDownValues(Path, divId) {
    path = Path;
    $.ajax({
        type: 'get',
        url: '/GetFormsDropdown.aspx?path=' + path,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null && data.length > 0) {
                var html = "";
                for (var i = 0; i < data.length; i++) {
                    html += '<option value="' + data[i].option + '">' + data[i].option + '</option>';
                }
                $("#" + divId).append(html);
                $('.selectpicker').selectpicker('refresh');
            }

        },
    });

}
function directionRtl() {
    document.getElementById("errorId").style.direction = "rtl";
    document.getElementById("errorCaptchaId").style.direction = "rtl";
}
function cultureClick() {
    var cult = $(location).attr("href");
    var arr = cult.split('/');
    arr[3] = 'en';
    var cultUrl = arr.join("/");
    $("#cultClick").attr("href", cultUrl);
}



const target = document.querySelector('header');

$("#duraton").val($('.visit-park:checked').val());


function calendarFunc() {
    if ($("#calendarl").length > 0) {
        var calendarEl = document.getElementById('calendarl');
        var calendar = new FullCalendar.Calendar(calendarEl, {
            height: 'auto',
            locale: 'ar',
            isRTL: true,
            buttonText: { today: 'اليوم' },

            //dayMaxEventRows: true, // for all non-TimeGrid views
            dayMaxEventRows: 2,
            //views: {
            // dayGrid: {
            // dayMaxEventRows: 2 // adjust to 6 only for timeGridWeek/timeGridDay
            // }
            //},
            eventClick: function (info) {
                $('.fc-daygrid-day-events a.fc-daygrid-event').attr('href', 'javascript:');
                $('.fc-daygrid-event-harness a.fc-daygrid-event').attr('href', 'javascript:');
                $('#modalTitle').html(info.event.title);
                $('#modalBody').html(info.event.extendedProps.description);
                $('#eventUrl').attr('href', info.event.url);
                if (info.event.url == 'javascript:' || !info.event.url) {
                    $('#eventUrl').attr('target', "");
                } else {
                    $('#eventUrl').attr('target', " ");
                }
                $('.fullCalModal').modal();
                return false;
            },
            // eventLimit: 4,
            events: window.location.origin + '/GetAllEvents.aspx',

            eventContent: function (arg) {
                let arrayOfDomNodes = []
                // title event
                let titleEvent = document.createElement('div')
                if (arg.event._def.title) {
                    titleEvent.innerHTML = arg.event._def.title
                    titleEvent.classList = "fc-event-title fc-sticky"
                }
                // image event
                let imgEventWrap = document.createElement('div')
                if (arg.event.extendedProps.image_url) {
                    let imgEvent = '<img src="' + arg.event.extendedProps.image_url + '" >'
                    imgEventWrap.classList = "fc-event-img"
                    imgEventWrap.innerHTML = imgEvent;
                }
                arrayOfDomNodes = [titleEvent, imgEventWrap]
                return { domNodes: arrayOfDomNodes }
            },
        });
        calendar.render();
    }
}
function sessionStorageOnLoad() {
    $.ajax({
        type: 'get',
        url: '/GetTicketsItems.aspx',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null && data.length > 0) {
                for (var j = 0; j < data.length; j++) {
                    if (sessionStorage.getItem(data[j].KeyValue) == null) {
                        sessionStorage.setItem(data[j].KeyValue, 0 + ',' + data[j].ItemNo);
                    }
                    localStorage.setItem(data[j].KeyValue, data[j].UnitPrice);
                }
            }
        }
    });

}
//$('#firstTabTitle').click
//    (function () {

//    }
//);

function SessionStorage() {
    if (sessionStorage.length > 0) {
        for (var i = 0; i < sessionStorage.length; i++) {
            var keys = Object.keys(sessionStorage);
            $('#' + keys[i]).val(sessionStorage.getItem(keys[i]).split(',')[0]);
        }

    }
}

function CardSessionStorage() {
    var ticketdata = [];
    var obj = {
        itemNo: $("input[name='radio']:checked").val(),
        qty: "1"
    }
    ticketdata.push(obj);

    var mydata = { 'ticketdata': JSON.stringify(ticketdata) };
    $.ajax({
        type: 'get',
        url: '/GetPriceApi.aspx',
        contentType: "application/json; charset=utf-8",
        data: mydata,
        success: function (getPriceData) {
            if (getPriceData != "") {
                var priceData = JSON.parse(getPriceData);
                var totalAmount, discount, requestId;
                requestId = priceData.RequestID;
                totalAmount = priceData.TotalAmount;
                discount = priceData.TotalDiscount;

                $.ajax({
                    type: 'get',
                    url: '/GetTicketsItems.aspx?pagetype=UAEP.Ticketing_Card_Items',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null && data.length > 0) {
                            var subTotal = 0;
                            var htmls = "";
                            var html = "";
                            for (var j = 0; j < data.length; j++) {

                                if ($("input[name='radio']:checked").val() == data[j].ItemNo) {

                                    subTotal = parseInt(data[j].UnitPrice);


                                    htmls += '<tr>';
                                    htmls += '<td>' + data[j].TicketType + '</td>';
                                    htmls += '<td> AED ' + data[j].UnitPrice + '</td>';
                                    htmls += '<td>';
                                    htmls += '<div class="quantity float-none float-md-right">';
                                    htmls += '<span>1x</span>';
                                    htmls += '<a class="change-btn loyal-prev-step" href="javascript:">' + 'Change' + '</a>';
                                    //htmls += '<a data-key="' + Keys[j] + '" class="del-btn" onClick="DeleteButton(this)" href="javascript:"><img src="/assets/svgs/dustbin.svg" alt=""></a>';
                                    htmls += '</div>';
                                    htmls += '</td>';
                                    htmls += '</tr>';
                                    localStorage.setItem("ClubCode", data[j].ClubCode);
                                    localStorage.setItem("KeyValue", data[j].KeyValue);
                                }



                            }
                            html += '<li>';
                            html += '<span>Subtotal </span>';
                            html += '<div class="dots"></div>';
                            html += '<span id="subtotal" data-subtotal="' + subTotal + '"> AED ' + subTotal + '</span>';
                            html += '</li>';
                            html += '<li>';
                            html += '<span>Total Discount</span>';
                            html += '<div class="dots"></div>';
                            html += '<span id="discount" data-discount="' + discount + '"> AED ' + discount + '</span>';
                            html += '</li>';
                            html += '<li>';
                            html += '<span>Total Amount</span>';
                            html += '<div class="dots"></div>';
                            html += '<span id="totalamount" data-totalamount="' + totalAmount + '"> AED ' + totalAmount + '</span>';
                            html += '</li>';
                            html += '<input id="orderid" style="display: none;" value="' + requestId + '">';
                            $('#SecondTabSecondScreenTable').html(htmls);
                            $('#SecondTabTotalSummary').html(html);

                            localStorage.setItem("OrderId", requestId);

                            $(".loyal-prev-step").on("click", function (e) {
                                e.preventDefault();
                                $loyalityForm.steps("previous");
                            });

                        }
                    }
                });
            }
        }
    });

}

var Items = [];
function SessionKeysAndValues() {
    var Keys = Object.keys(sessionStorage);
    var Values = Object.values(sessionStorage);

    var ticketdata = [];
    for (var i = 0; i < Values.length; i++) {
        var obj = {
            itemNo: Values[i].split(',')[1],
            qty: Values[i].split(',')[0]
        }
        ticketdata.push(obj);
    }

    var mydata = {
        'ticketdata': JSON.stringify(ticketdata.sort(function (a, b) {
            return a.itemNo - b.itemNo
        }))
    };
    $.ajax({
        type: 'get',
        url: '/GetPriceApi.aspx',
        contentType: "application/json; charset=utf-8",
        data: mydata,
        success: function (getPriceData) {
            if (getPriceData != "") {
                var priceData = JSON.parse(getPriceData);
                var totalAmount, discount, requestId;
                requestId = priceData.RequestID;
                totalAmount = priceData.TotalAmount;
                discount = priceData.TotalDiscount;
                $.ajax({
                    type: 'get',
                    url: '/GetTicketsItems.aspx',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null && data.length > 0) {
                            var itemData = sessionStorage;
                            var subTotal = 0;
                            var itemDiscount = 0;
                            var htmls = "";
                            var html = "";
                            for (var i = 0; i < data.length; i++) {
                                for (var j = 0; j < sessionStorage.length; j++) {
                                    if (Keys[j] == data[i].KeyValue) {
                                        if (Values[j].split(',')[0] > 0) {

                                            subTotal += parseInt(data[i].UnitPrice) * parseInt(Values[j]);
                                            htmls += '<tr>';
                                            htmls += '<td>' + data[i].TicketType + '</td>';
                                            htmls += '<td> AED ' + data[i].UnitPrice + '</td>';
                                            htmls += '<td>';
                                            htmls += '<div class="quantity float-none float-md-right">';
                                            htmls += '<span>' + Values[j].split(',')[0] + 'x</span>';
                                            htmls += '<a class="change-btn ticketsprev-step" href="javascript:">' + 'Change' + '</a>';
                                            //htmls += '<a data-key="' + Keys[j] + '" class="del-btn" onClick="DeleteButton(this)" href="javascript:"><img src="/assets/svgs/dustbin.svg" alt=""></a>';
                                            htmls += '</div>';
                                            htmls += '</td>';
                                            htmls += '</tr>';

                                            //if (priceData.Items.length < i) {

                                            if (priceData.Items[i].DiscountAmount != "0") {
                                                itemDiscount = parseFloat(priceData.Items[i].DiscountAmount) / parseInt(Values[j].split(',')[0]);
                                                localStorage.setItem(data[i].KeyValue + "ItemDiscount", itemDiscount);
                                            }
                                            //}

                                        }

                                    }
                                }
                            }

                            html += '<li>';
                            html += '<span>Subtotal </span>';
                            html += '<div class="dots"></div>';
                            html += '<span id="subtotal" data-subtotal="' + subTotal + '"> AED ' + subTotal + '</span>';
                            html += '</li>';
                            html += '<li>';
                            html += '<span>Total Discount</span>';
                            html += '<div class="dots"></div>';
                            html += '<span id="discount" data-discount="' + discount + '"> AED ' + discount + '</span>';
                            html += '</li>';
                            html += '<li>';
                            html += '<span>Total Amount</span>';
                            html += '<div class="dots"></div>';
                            html += '<span id="totalamount" data-totalamount="' + totalAmount + '"> AED ' + totalAmount + '</span>';
                            html += '</li>';
                            html += '<input id="orderid" style="display: none;" value="' + requestId + '">';
                            $('#SecondScreenTable').html(htmls);
                            $('#TotalSummary').html(html);

                            localStorage.setItem("OrderId", requestId);

                            $(".ticketsprev-step").on("click", function (e) {
                                e.preventDefault();
                                $paymentMethodForm.steps("previous");
                            });
                            $("#getpricesecondscreen").removeAttr("style");
                        }

                    }
                });

            } else if (getPriceData == "") {                     //ApiUrlIsNotWorking

                var htmls = "";
                htmls += '<tr><td></td><td>هناك خطأ ما. يرجى إعادة تحميل صفحتك وحاول مرة أخرى.</td><td></td></tr>';
                $('#SecondScreenTable').html(htmls);
                console.log("Pos Api Url is not working");

            }
        },

        error: function (err) {

        }
    });


}

function DeleteButton(el) {

    if (el.dataset.key == "adult") {
        $('#AdultTicket').modal('show');
        return false;
    }
    sessionStorage.removeItem(el.dataset.key);
    el.parentElement.parentElement.parentElement.remove()
    SessionKeysAndValues();
    sessionStorageOnLoad();
}

function BusinessLogicsMax() {
    var currentTicket = 0;
    var totalTickets = 0;
    var Keys = Object.keys(sessionStorage);
    var Values = Object.values(sessionStorage);
    for (var i = 0; i < sessionStorage.length; i++) {

        if (Values[i].split(',')[0] > 0) {
            currentTicket += parseInt(Values[i].split(',')[0]);
        }
    }
    totalTickets = parseInt(currentTicket) + 1;
    if (totalTickets > 10) {
        return false;
    }
    return true;
}

function BusinessLogicsAdult() {
    var Values = Object.values(sessionStorage);
    var totalCount = 0;
    for (var i = 0; i < sessionStorage.length; i++) {
        totalCount += parseInt(Values[i].split(',')[0]);
    }
    if (totalCount < 1) { //Values[i].split(',')[1] == 1 &&
        var msg = "oneAdult";
        $('#AdultTicket').modal('show');
        return false;
    }

    return true;
}

function SelectIndex() {
    if ($(".bootstrap-select").length > 0) {
        console.log("Hello World");
        $('.selectpicker').on('show.bs.select', function (e, clickedIndex, isSelected, previousValue) {
            $(".select-field").removeClass('upper-index');
            $(this).parents(".select-field").addClass('upper-index');
        });
    }
}
$(document).ready(function () {

    SizeInKb();
    cultureClick();
    faqDropdownSlider();
    sessionStorageOnLoad();
    SessionStorage();

    menuButton.click(function () {
        $('html').toggleClass('open-html');
        $('.menu-open').toggleClass('click');
        $('.primary-menu').toggleClass('open-menu');
        if ($(this).hasClass("click")) {
            scroll.stop();
        }
        else {
            scroll.start();
        }
    });

    $('.fc-prev-button, .fc-next-button').on('click', function () {
        window.dispatchEvent(new Event('resize'));
    });

    //FOR PLACEHOLDER OF FORMS ID AND NAME NOT CHANGE
    $("#inp").prop("name", "email");
    $("#np").prop("name", "name");
    $("#nation").prop("name", "nationality");
    $("#fnp").prop("name", "Fname");
    $("#lnp").prop("name", "Lname");
    $("#ageplaceholder").prop("name", "age");
    $("#cit").prop("name", "city");
    $("#em").prop("name", "email");
    $("#cmnts").prop("name", "comments");
    $("#mssg").prop("name", "message");
    $("#cnam").prop("name", "Cname");
    $("#eventDatepicker").prop("name", "date");
    $("#txtcaptcha").prop("name", "captcha");
    $("#txtcaptchas").prop("name", "captcha");
    $("#hdnNodePathBookingtype").prop("name", "hdnNodePathBookingtype");
    $("#hdnNodePath").prop("name", "hdnNodePath");
    $("#hdnNodePath1").prop("name", "hdnNodePath1");
    $("#hdnNodePath2").prop("name", "hdnNodePath2");
    $("#hdnNodePathAge").prop("name", "hdnNodePathAge");
    $("#hdnNodePathCorporate1").prop("name", "hdnNodePathCorporate1");
    $("#hdnNodePathCorporate2").prop("name", "hdnNodePathCorporate2");
    $("#hdnNodePathTripType").prop("name", "hdnNodePathTripType");
    $("#hdnNodePathTripTime").prop("name", "hdnNodePathTripTime");
    $("#idselection").prop("name", "BookingTypeOption");
    $("#rnp").prop("name", "Rname");
    $("#scp").prop("name", "Schname");
    $("#nosp").prop("name", "Numstd");
    $("#notesp").prop("name", "message");


    path = "";
    path = $("#hdnNodePathBookingtype").val();
    if (path) {
        path = $("#hdnNodePathBookingtype").val();
        DropDownValues(path, 'idselection');
    }
    path = "";
    path = $("#hdnNodePath").val();
    if (path) {
        path = $("#hdnNodePath").val();
        DropDownValues(path, 'query');
    }
    path = "";
    path = $("#hdnNodePath1").val();
    if (path) {
        path = $("#hdnNodePath1").val();
        DropDownValues(path, 'eventType');
    }
    path = "";
    path = $("#hdnNodePath2").val();
    if (path) {
        path = $("#hdnNodePath2").val();
        DropDownValues(path, 'time');
    }

    path = "";
    path = $("#hdnNodePathCorporate1").val();
    if (path) {
        path = $("#hdnNodePathCorporate1").val();
        DropDownValues(path, 'query1');
    }

    path = "";
    path = $("#hdnNodePathCorporate2").val();
    if (path) {
        path = $("#hdnNodePathCorporate2").val();
        DropDownValues(path, 'eventTypeCorporate');
    }

    path = "";
    path = $("#hdnNodePathAge").val();
    if (path) {
        path = $("#hdnNodePathAge").val();
        DropDownValues(path, 'agee');
    }
    path = "";
    path = $("#hdnNodePathTripType").val();
    if (path) {
        path = $("#hdnNodePathTripType").val();
        DropDownValues(path, 'tripType');
    }
    path = "";
    path = $("#hdnNodePathTripTime").val();
    if (path) {
        path = $("#hdnNodePathTripTime").val();
        DropDownValues(path, 'tripTime');
    }

    $(".back-to-top").on("click", function () {
        scroll.scrollTo(target);
    });

    $(".sub-menu-icon").on("click", function () {
        $(this).parent().find(".submenu").slideToggle();
    });

    $(document).on('click', '.ui-state-default', function (e) {
        e.preventDefault();
    });

    if ($venuedetailSlider.length > 0) {
        $venuedetailSlider.slick({
            arrows: true,
            rtl: true,
            slidesToShow: 1,
            prevArrow: '<a href="javascript:" class="happening-arrow happening-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="happening-arrow happening-arrow-right"></a>',
        });
    }


    //$('#nav-profile-tab').click(function () {
    //    sessionStorage.clear();

    //});
    //$('#nav-home-tab').click(function () {
    //    sessionStorageOnLoad();
    //    SessionStorage();
    //});
    if ($("#bavform").length > 0) {
        $("#voiindividualremove").hide();
        $("#etindividualremove").hide();
        $("#cnremove").hide();
        $("#etcorporateremove").hide();
        $("#voicorpaorateremove").hide();
    }

    if ($wisdomTxtSlider.length > 0) {
        $wisdomTxtSlider
            .on("beforeChange", function (event, slick, currentSlide, nextSlide) {
                const currentCount = nextSlide + 1,
                    NumberCount = String("00" + currentCount).slice(-2);
                $(".wisdom-slide-Count span").text(NumberCount);
            })
            .on("init", function (event, slick) {
                const currentCount = slick.currentSlide + 1,
                    NumberCount = String("00" + currentCount).slice(-2);
                $(".wisdom-slide-Count span").text(NumberCount);
                //$(".wisdom-slide-Count span").text(slick.currentSlide + 1);
            })
            .slick({
                rtl: true,
                slidesToShow: 1,
                slidesToScroll: 1,
                dots: false,
                arrows: true,
                infinite: false,
                asNavFor: ".wisdom-sliders-part",
                prevArrow: $(".wisdom-arrow-left"),
                nextArrow: $(".wisdom-arrow-right")
            });
        $wisdomImgSlider.slick({
            rtl: true,
            slidesToShow: 1,
            slidesToScroll: 1,
            asNavFor: ".wisdom-sliders-part",
            dots: false,
            arrows: false,
            fade: true,
            infinite: false,
            speed: $("html").hasClass("ie") ? 1000 : 0
        })
            .on('beforeChange', function (event, slick, currentSlide, nextSlide) {
                if (!$("html").hasClass("ie")) {
                    var prevImg = $(slick.$slides[currentSlide]).find(".main-img").attr("src");
                    $(slick.$slides[nextSlide]).find(".cover-img>img").attr("src", prevImg);
                }
            })
            .on('afterChange', function (event, slick, currentSlide, nextSlide) {
                // $(".page-view .project").removeClass("hide");
            });
        $WisdomIconSlider.slick({
            rtl: true,
            slidesToShow: 8,
            centerMode: true,
            asNavFor: ".wisdom-sliders-part",
            dots: false,
            arrows: false,
            infinite: false,
            focusOnSelect: true,
            responsive: [
                {
                    breakpoint: 676,
                    settings: {
                        slidesToShow: 3
                    }
                }
            ]
        });
    }


    if ($happeningSlider.length > 0) {
        $happeningSlider.slick({
            rtl: true,
            centerMode: true,
            arrows: true,
            infinite: true,
            slidesToShow: 1,
            centerPadding: '160px',
            prevArrow: '<a href="javascript:" class="happening-arrow happening-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="happening-arrow happening-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 1200,
                    settings: {
                        slidesToShow: 2,
                        centerMode: false,
                        centerPadding: '0',
                    }
                },
                {
                    breakpoint: 992,
                    settings: {
                        slidesToShow: 2,
                        centerMode: false,
                        centerPadding: '0',
                    }
                },
                {
                    breakpoint: 768,
                    settings: {
                        slidesToShow: 1,
                        centerMode: false,
                    }
                },
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                        centerMode: false,
                    }
                }
            ]
        });
    }


    if ($activitySlider.length > 0) {
        $activitySlider.slick({
            rtl: true,
            slidesToShow: 1,
            prevArrow: '<a href="javascript:" class="activity-arrow activity-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="activity-arrow activity-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 768,
                    settings: {
                        slidesToShow: 1,
                    }
                },
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                    }
                }
            ]
        });
    }

    if ($missionSlider.length > 0) {
        $missionSlider.slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            infinite: false,
            rtl: true,
            prevArrow: '<a href="javascript:" class="activity-arrow activity-arrow-left slick-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="activity-arrow activity-arrow-right slick-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 992,
                    settings: {
                        slidesToShow: 2,
                    }
                },
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                    }
                }
            ]
        });
    }

    if ($factsSlider.length > 0) {
        $factsSlider.slick({
            slidesToShow: 4,
            slidesToScroll: 1,
            infinite: false,
            rtl: true,
            prevArrow: '<a href="javascript:" class="activity-arrow facts-arrow-left slick-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="activity-arrow facts-arrow-right slick-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 992,
                    settings: {
                        slidesToShow: 3,
                    }
                },
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                    }
                }
            ]
        });
    }

    if ($guinnessSlider.length > 0) {
        if ($(window).width() > 767) {
            var rowCount = 2;
        } else {
            var rowCount = 1;
        }
        $guinnessSlider.slick({
            rows: rowCount,
            slidesToShow: 1,
            slidesToScroll: 1,
            infinite: false,
            variableWidth: true,
            rtl: true,
            prevArrow: '<a href="javascript:" class="activity-arrow facts-arrow-left slick-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="activity-arrow facts-arrow-right slick-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 992,
                    settings: {
                        slidesToShow: 1,
                    }
                },
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                        variableWidth: false
                    }
                }
            ]
        });
    }

    if ($("a[data-toggle=\"tab\"]").length > 0) {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            $guinnessSlider.slick('setPosition');
            $factsSlider.slick('setPosition');
            scroll.update();
        })
    }




    if ($partnersSlider.length > 0) {
        $partnersSlider.slick({
            slidesToShow: 5,
            slidesToScroll: 1,
            infinite: false,
            rtl: true,
            dots: true,
            arrows: false,
            responsive: [
                {
                    breakpoint: 992,
                    settings: {
                        slidesToShow: 2,
                    }
                },
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                    }
                }
            ]
        });
    }


    $("#NewsLetterform").wrap("<form  id='newsletter'></form>");
    if ($("#newsletter").length > 0) {
        $("#newsletter").formValidation({
            fields: {
                email: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }, regexp: {
                            regexp: emailRegex,
                            message: ' '
                        }
                    }
                }
            },
            onSuccess: function (e) {
                e.preventDefault();
                var $form = $(e.target),
                    formId = '#' + $form[0].id;
                $(formId).addClass('loading').append('<div class="loader"></div>');
                $.ajax({
                    type: 'post',
                    url: '/postaspxfiles/newsletter/post.aspx ',
                    data: $(formId).serialize(),
                    success: function (data) {
                        if (data == "success") {
                            setTimeout(function () {
                                var fields = $(formId).data('formValidation').getOptions().fields,
                                    $parent, $icon;
                                for (var field in fields) {
                                    $parent = $('[name="' + field + '"]').parents('.form-group');
                                }
                                // Then reset the form
                                $('.alert-success').addClass('in');
                                $(formId).data('formValidation').resetForm(true);
                                $("input[type=text], textarea").val("");
                                $('.file-input-name').hide();
                                $('#NewsLetterform .thanks').show();
                                scroll.scrollTo('#NewsLetterform .thanks-inner');
                                $(formId).removeClass('loading');
                                $(formId).find('.loader').remove();
                                setTimeout(function () {
                                    $('#NewsLetterform .thanks').hide();
                                    $(formId).data('formValidation').resetForm(true);
                                }, 5000);
                            }, 1500);
                        }
                        else if (data == "alreadyExist") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            $(".alreadyExist").show();
                            setTimeout(function () {
                                $("input[type=text]").val("");
                                $(".alreadyExist").hide();
                                refresh();
                            }, 4000);
                        }
                    }

                });
            }
        });
    }

    $('.captcha-img').click(function () {
        refresh();
    });
    refresh();
    function refresh() {
        $("#imgCaptcha").attr("src", "/FormCaptcha.aspx?" + new Date().getMilliseconds());
        $("#imgCaptchas").attr("src", "/FormCaptchaCard.aspx?" + new Date().getMilliseconds());
    }


    $("#contactform").wrap("<form  id='cnt-form'></form>");
    if ($("#cnt-form").length > 0) {

        $("#cnt-form").formValidation({
            fields: {
                Fname: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                Lname: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                email: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }, regexp: {
                            regexp: emailRegex,
                            message: ' '
                        }
                    }
                },
                number: {
                    validators: {
                        regexp: { regexp: phone_numberRegex, message: ' ' }
                    }
                },

                city: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                question: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                message: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                captcha: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                }
            },

            onSuccess: function (e) {

                e.preventDefault();
                var $form = $(e.target),
                    formId = '#' + $form[0].id;
                var data = $(formId).serialize() + "&telCode=" + $(".iti__selected-dial-code").text();
                $(formId).addClass('loading').append('<div class="loader"></div>');
                $.ajax({
                    type: 'post',
                    url: '/postaspxfiles/post.aspx ',
                    data: data,
                    success: function (data) {
                        if (data == "success") {
                            setTimeout(function () {
                                var fields = $(formId).data('formValidation').getOptions().fields,
                                    $parent, $icon;
                                for (var field in fields) {
                                    $parent = $('[name="' + field + '"]').parents('.form-group');
                                }
                                // Then reset the form
                                $('.alert-success').addClass('in');
                                $(formId).data('formValidation').resetForm(true);
                                $("input[type=text], textarea").val("");
                                $('select[id=query]').val("");
                                $('.selectpicker').selectpicker('refresh');
                                $('.file-input-name').hide();
                                $('#contactform .thanks').show();
                                scroll.scrollTo('#contactform .thanks-inner');
                                $(formId).removeClass('loading');
                                $(formId).find('.loader').remove();
                                setTimeout(function () {
                                    $('#contactform .thanks').hide();
                                    $(formId).data('formValidation').resetForm(true);
                                    scroll.scrollTo('.contact-form-sec');
                                    refresh();
                                }, 5000);
                            }, 1500);
                        } else if (data == "invalid") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            directionRtl();
                            $("input[type=tel]").removeClass('error');
                            $('#captchadiv').removeClass('has-success');
                            $('#captchadiv').addClass('has-error');
                            $(".errorCaptcha").show();
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                            }, 4000);
                        } else if (data == "fail") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            directionRtl();
                            $(".error").text();
                            $(".error").show();
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                            }, 4000);
                        }
                    },
                    error: function (e) {
                        $(formId).find('.loader').remove();
                        $(formId).data('formValidation').resetForm(true);
                        refresh();
                    }
                });
            }
        });
    }


    $("#bavform").wrap("<form  id='bav-form'></form>");
    if ($("#bav-form").length > 0) {
        $("#bav-form").formValidation({
            fields: {
                Fname: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                Lname: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                //Cname: {
                //    validators: {
                //        notEmpty: {
                //            message: ' '
                //        },
                //        regexp: {
                //            regexp: letters,
                //            message: ' '
                //        }
                //    }
                //},
                date: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                time: {

                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                email: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }, regexp: {
                            regexp: emailRegex,
                            message: ' '
                        }
                    }
                },
                number: {
                    validators: {
                        regexp: { regexp: phone_numberRegex, message: ' ' }
                    }
                },
                type: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                BookingTypeOption: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                question: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                message: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                captcha: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                }
            },
            onSuccess: function (e) {

                e.preventDefault();
                var $form = $(e.target),
                    formId = '#' + $form[0].id;
                var data = $(formId).serialize() + "&telCode=" + $(".iti__selected-dial-code").text();
                $(formId).addClass('loading').append('<div class="loader"></div>');
                $.ajax({
                    type: 'post',
                    url: '/postaspxfiles/BookAVenue/post.aspx ',
                    data: data,
                    success: function (data) {

                        if (data == "success") {
                            setTimeout(function () {
                                var fields = $(formId).data('formValidation').getOptions().fields,
                                    $parent, $icon;
                                for (var field in fields) {
                                    $parent = $('[name="' + field + '"]').parents('.form-group');
                                }
                                // Then reset the form
                                $('.alert-success').addClass('in');
                                $(formId).data('formValidation').resetForm(true);
                                $("input[type=text], textarea").val("");
                                $('select[id=query]').val(" ");
                                $('select[id=time]').val(" ");
                                $('select[id=eventType]').val(" ");
                                $('select[id=query1]').val(" ");
                                $('select[id=eventTypeCorporate]').val(" ");
                                $('select[id=idselection]').val(" ");
                                $('.selectpicker').selectpicker('refresh');
                                $('.file-input-name').hide();
                                $('#bavform .thanks').show();
                                scroll.scrollTo('#bavform .thanks-inner');
                                $(formId).removeClass('loading');
                                $(formId).find('.loader').remove();
                                if ($(window).width() < 768) { $('html, body').animate({ scrollTop: $('.contact-form').offset().top }); }
                                setTimeout(function () {
                                    $('#bavform .thanks').hide();
                                    $(formId).data('formValidation').resetForm(true);
                                    scroll.scrollTo('.contact-form-sec');
                                }, 5000);
                            }, 1500);
                        } else if (data == "invalid") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            directionRtl();
                            $("input[type=tel]").removeClass('error');
                            $('#captchadiv').removeClass('has-success');
                            $('#captchadiv').addClass('has-error');
                            $(".errorCaptcha").show();
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                            }, 4000);
                        } else if (data == "fail") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            directionRtl();
                            $(".error").text();
                            $(".error").show();
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                            }, 4000);
                        } else if (data == "TimeError") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            document.getElementById("TimeErrorId").style.direction = "rtl";
                            $('#TimeDiv').addClass('has-error');
                            $('#captchadiv').addClass('has-error');
                            $(".TimeError").text();
                            $(".TimeError").show();
                            setTimeout(function () {
                                $(".TimeDiv").hide();
                                $(".errorCaptcha").hide();
                                $(".TimeError").hide();
                                $(".error").hide();
                                refresh();
                            }, 4000);
                        }
                    },
                    error: function (e) {
                        $(formId).find('.loader').remove();
                        $(formId).data('formValidation').resetForm(true);
                        refresh();
                    }

                });
            }
        });
    }

    //if ($("#idselection").val() == 'individual') {
    //    $("#cnremove").hide();        
    //    $("#etcorporateremove").hide();
    //    $("#voicorpaorateremove").hide();


    //} else {
    //    $("#cnremove").show();
    //    $("#etcorporateremove").show();
    //    $("#voicorpaorateremove").show();
    //}

    $('#idselection').change(function () {
        if ($("#idselection").val() == 'individual') {
            $("#voiindividualremove").show();
            $("#etindividualremove").show();
            $("#cnremove").hide();
            $("#etcorporateremove").hide();
            $("#voicorpaorateremove").hide();
            $('select[id=query1]').val(" ");
            $('select[id=eventTypeCorporate]').val(" ");
            $('.selectpicker').selectpicker('refresh');

        } else if ($("#idselection").val() == 'corporate') {
            //$("#cnremove").show();
            $("#etcorporateremove").show();
            $("#voicorpaorateremove").show();
            $("#voiindividualremove").hide();
            $("#etindividualremove").hide();
            $('select[id=query]').val(" ");
            $('select[id=eventType]').val(" ");
            $('.selectpicker').selectpicker('refresh');

        } else if ($("#idselection").val() == ' ') {
            $("#voiindividualremove").hide();
            $("#etindividualremove").hide();
            $("#cnremove").hide();
            $("#etcorporateremove").hide();
            $("#voicorpaorateremove").hide();
        }
    });

    $("#school-form").wrap("<form  id='primary-school-form'></form>");
    if ($("#primary-school-form").length > 0) {
        $("#primary-school-form").formValidation({
            fields: {
                Rname: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                Schname: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                Numstd: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                date: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                time: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                email: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }, regexp: {
                            regexp: emailRegex,
                            message: ' '
                        }
                    }
                },
                number: {
                    validators: {
                        regexp: { regexp: phone_numberRegex, message: ' ' }
                    }
                },
                type: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                },
                captcha: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                }
            },
            onSuccess: function (e) {
                e.preventDefault();
                var $form = $(e.target),
                    formId = '#' + $form[0].id;
                var data = $(formId).serialize() + "&telCode=" + $(".iti__selected-dial-code").text();
                $(formId).addClass('loading').append('<div class="loader"></div>');
                $.ajax({
                    type: 'post',
                    url: '/postaspxfiles/SchoolTrips/post.aspx',
                    data: data,
                    success: function (data) {
                        if (data == "success") {
                            setTimeout(function () {
                                var fields = $(formId).data('formValidation').getOptions().fields,
                                    $parent, $icon;
                                for (var field in fields) {
                                    $parent = $('[name="' + field + '"]').parents('.form-group');
                                }
                                // Then reset the form
                                $('.alert-success').addClass('in');
                                $(formId).data('formValidation').resetForm(true);
                                $("input[type=text], textarea").val("");
                                $('select[id=tripType]').val(" ");
                                $('select[id=tripTime]').val(" ");
                                $('.selectpicker').selectpicker('refresh');
                                $('.file-input-name').hide();
                                $('#school-form .thanks').show();
                                scroll.scrollTo('#school-form .thanks-inner');
                                $(formId).removeClass('loading');
                                $(formId).find('.loader').remove();
                                if ($(window).width() < 768) {
                                    $('html, body').animate({ scrollTop: $('.contact-form').offset().top });
                                }
                                setTimeout(function () {
                                    $('#school-form .thanks').hide();
                                    $(formId).data('formValidation').resetForm(true);
                                    scroll.scrollTo('.contact-form-sec');
                                }, 5000);
                            }, 1500);
                        } else if (data == "invalid") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            $("input[type=tel]").removeClass('error');
                            $('#captchadiv').removeClass('has-success');
                            $('#captchadiv').addClass('has-error');
                            $(".errorCaptcha").show();
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                            }, 4000);
                        } else if (data == "fail") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            $(".error").text();
                            $(".error").show();
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                            }, 4000);
                        } else if (data == "TimeError") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            $('#TimeDiv').addClass('has-error');
                            $('#captchadiv').addClass('has-error');
                            $(".TimeError").text();
                            $(".TimeError").show();
                            setTimeout(function () {
                                $(".TimeDiv").hide();
                                $(".errorCaptcha").hide();
                                $(".TimeError").hide();
                                $(".error").hide();
                                refresh();
                            }, 4000);
                        }
                    },
                    error: function (e) {
                        $(formId).find('.loader').remove();
                        $(formId).data('formValidation').resetForm(true);
                        refresh();
                    }

                });
            }
        });
    }



    if ($indForm.length > 0) {
        $indForm
            .formValidation({
                fields: {
                    Fname: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    Lname: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    Cname: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    date: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    time: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    email: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }, regexp: {
                                regexp: emailRegex,
                                message: ' '
                            }
                        }
                    },
                    number: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    city: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    question: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    message: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    captcha: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    }
                },
                onSuccess: function (e) {
                    e.preventDefault();
                    var $form = $(e.target),
                        formId = '#' + $form[0].id;
                    $(formId).addClass('loading').append('<div class="loader"></div>');
                    $.ajax({
                        type: 'post',
                        url: ' ',
                        data: $(formId).serialize(),
                        success: function () {
                            setTimeout(function () {
                                var fields = $(formId).data('formValidation').getOptions().fields,
                                    $parent, $icon;
                                for (var field in fields) {
                                    $parent = $('[name="' + field + '"]').parents('.form-group');
                                }
                                // Then reset the form
                                $('.alert-success').addClass('in');
                                $(formId).data('formValidation').resetForm(true);
                                $("input[type=text], textarea").val("");
                                $('.file-input-name').hide();
                                $('.thanks').show();
                                $(formId).removeClass('loading');
                                $(formId).find('.loader').remove();
                                setTimeout(function () {
                                    $('.thanks').hide();
                                    $(formId).data('formValidation').resetForm(true);
                                }, 5000);
                            }, 1500);
                        }
                    });
                }
            });
    }
    $("#paymentForm").wrap("<form  id='payment-method-form' class='select-ticket-tab'></form>");
    $paymentMethodForm = $("#payment-method-form");
    if ($paymentMethodForm.length > 0) {

        $('#paymentForm').contents().unwrap();
        function adjustIframeHeight() {
            var $body = $('body'),
                $iframe = $body.data('iframe.fv');
            if ($iframe) {
                $iframe.height($body.height());
            }
        }

        $paymentMethodForm
            .steps({
                headerTag: ".tickets-tab-steps",
                bodyTag: ".select-ticket-content",
                enablePagination: false,
                enableFinishButton: false,
                onStepChanged: function (e, currentIndex, priorIndex) {
                    adjustIframeHeight();
                },
                onStepChanging: function (e, currentIndex, newIndex) {
                    var fv = $paymentMethodForm.data('formValidation'),
                        $container = $paymentMethodForm.find('.select-ticket-content[data-step="' + currentIndex + '"]');
                    fv.validateContainer($container);
                    var isValidStep = fv.isValidContainer($container);
                    if (isValidStep === false || isValidStep === null) {
                        return false;
                    }

                    SessionStorage();
                    SessionKeysAndValues();
                    if (BusinessLogicsAdult() == false) {
                        return false;
                    }
                    return true;
                },
                onFinishing: function (e, currentIndex) {
                    var fv = $paymentMethodForm.data('formValidation'),
                        $container = $paymentMethodForm.find('.select-ticket-content[data-step="' + currentIndex + '"]');
                    fv.validateContainer($container);
                    var isValidStep = fv.isValidContainer($container);
                    if (isValidStep === false || isValidStep === null) {
                        return false;
                    }
                    return true;
                },
                onFinished: function (event, currentIndex) {
                    console.log("Submitted!");
                }
            });

        //Date Validation For One Month
        if ($('#ticketDatepicker').length > 0) {

            var dateToday = new Date();
            $('#ticketDatepicker').datepicker({
                minDate: dateToday,
                maxDate: '+1m',
                beforeShow: function (input, inst) {
                    scroll.stop();
                    $('#ui-datepicker-div').removeClass(function () {
                        return $('input').get(0).id;
                    });
                    $('#ui-datepicker-div').addClass(this.id);
                },
                onSelect: function (dateText, inst) {
                    scroll.start();
                },
                onClose: function (dateText, inst) {
                    if ($("#bav-form").length) {
                        $("#bav-form").data('formValidation').revalidateField('date');
                    }

                }
            });
        }

        //defaultDate
        $("#ticketDatepicker").datepicker().datepicker("setDate", new Date());

        $paymentMethodForm
            .formValidation({
                framework: 'bootstrap',
                // This option will not ignore invisible fields which belong to inactive panels
                excluded: ':disabled',
                fields: {
                    Fullname: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            },
                            regexp: {
                                regexp: letters,
                                message: ' '
                            }
                        }
                    },
                    email: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }, regexp: {
                                regexp: emailRegex,
                                message: ' '
                            }
                        }
                    },
                    number: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            },
                            regexp: { regexp: phone_numberRegex, message: ' ' }
                        }
                    },
                    date: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    TermsCheckBox: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    captcha: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    }
                },
                onSuccess: function (e) {
                    e.preventDefault();
                    var keys = Object.keys(sessionStorage);
                    var values = Object.values(sessionStorage);
                    var tickets = "";
                    for (var i = 0; i < keys.length; i++) {
                        tickets += keys[i] + ":" + values[i].split(',')[0] + ",";
                    }

                    var $form = $(e.target),
                        formId = '#' + $form[0].id;
                    var data = $(formId).serialize() + "&telCode=" + $(".iti__selected-dial-code").text() + "&status=" + "Pending" + "&orderid=" + $('#orderid').val() + "&subtotal=" + $('#subtotal').data('subtotal') + "&totalamount=" + $('#totalamount').data('totalamount') + "&discount=" + $('#discount').data('discount') + "&tickets=" + tickets;
                    $(formId).addClass('loading').append('<div class="loader"></div>');
                    $.ajax({
                        type: 'post',
                        url: '/postaspxfiles/PaymentForm/post.aspx ',
                        data: data,
                        success: function (objData) {
                            if (objData != null && objData != undefined) {
                                var data = JSON.parse(objData);
                                if (data.status == "success") {
                                    $("#paymentPortalUrlDiv").html("");
                                    var htmlPaymentPortal = "<form action='" + data.registrationResponseData.Transaction.PaymentPortal + "' method='post'>";
                                    htmlPaymentPortal += "<input type='Hidden' name='TransactionID' value='" + data.registrationResponseData.Transaction.TransactionID + "'/>";
                                    htmlPaymentPortal += "<p class='animate fadeInUp animated' data-animation='fadeInUp' data-duration='400'>Please click on the Button to Proceed With Payment</p>";
                                    htmlPaymentPortal += "<input type='submit' class='btn btn-outline-green btn-pay' value='Submit'>";
                                    htmlPaymentPortal += "</form>";
                                    localStorage.setItem("TransactionId", data.registrationResponseData.Transaction.TransactionID);

                                    $("#paymentPortalUrlDiv").append(htmlPaymentPortal);
                                    $(formId).removeClass('loading');
                                    $(formId).find('.loader').remove();
                                    $('#PaymentPortalButton').modal('show');
                                    $('#paymentButton').removeAttr("disabled");
                                    //$("#payment-method-form-p-2").hide();
                                    //$("#paymentPortalUrlDiv").show();


                                    //setTimeout(function () {
                                    //    var fields = $(formId).data('formValidation').getOptions().fields,
                                    //        $parent, $icon;
                                    //    for (var field in fields) {
                                    //        $parent = $('[name="' + field + '"]').parents('.form-group');
                                    //    }
                                    //    // Then reset the form
                                    //    $('.alert-success').addClass('in');
                                    //    $(formId).data('formValidation').resetForm(true);
                                    //    $("input[type=text], textarea").val("");
                                    //    $('.file-input-name').hide();
                                    //    $('.contact-form').hide();
                                    //    $('.thanks').show();
                                    //    $(formId).removeClass('loading');
                                    //    $(formId).find('.loader').remove();
                                    //    setTimeout(function () {
                                    //        $('.contact-form').show();
                                    //        $('.thanks').hide();
                                    //        $(formId).data('formValidation').resetForm(true);
                                    //    }, 5000000);
                                    //}, 1500);
                                } else if (data.status == "invalid") {
                                    $(formId).find('.loader').remove();
                                    $(formId).removeClass('loading');
                                    $("input[type=tel]").removeClass('error');
                                    $('#captchadiv').removeClass('has-success');
                                    $('#captchadiv').addClass('has-error');
                                    $(".errorCaptcha").show();
                                    setTimeout(function () {
                                        $(".errorCaptcha").hide();
                                        $(".error").hide();
                                        refresh();
                                    }, 4000);
                                } else if (data.status == "fail") {
                                    $(formId).find('.loader').remove();
                                    $(formId).removeClass('loading');
                                    $(".error").text();
                                    $(".error").show();
                                    setTimeout(function () {
                                        $(".errorCaptcha").hide();
                                        $(".error").hide();
                                        refresh();
                                    }, 4000);
                                }
                            }
                        },
                        error: function (e) {
                            $(formId).find('.loader').remove();
                            $(formId).data('formValidation').resetForm(true);
                            refresh();
                        }
                    });
                }
            });

        $(".ticketsprev-step").on("click", function (e) {
            e.preventDefault();
            $paymentMethodForm.steps("previous");
        });
        $(".ticketsnext-step").on("click", function (e) {
            e.preventDefault();
            $paymentMethodForm.steps("next");
        });

    }
    var TransID = "";
    var Values = Object.values(sessionStorage);
    var Keys = Object.keys(sessionStorage);
    var numberOfItems = 0;
    for (var i = 0; i < Values.length; i++) {
        if (Values[i].split(',')[0] > 0) {
            numberOfItems += 1;
        }
    }
    localStorage.setItem("numberOfItems", numberOfItems);

    var ticketdata = [];
    for (var i = 0; i < Values.length; i++) {
        var obj = {
            itemNo: Values[i].split(',')[1],
            qty: Values[i].split(',')[0],
            unitprice: localStorage.getItem(Keys[i]),
            itemDiscount: localStorage.getItem(Keys[i] + "ItemDiscount")
        }
        ticketdata.push(obj);
    }


    var mydata = { 'ticketdata': JSON.stringify(ticketdata) };
    var formId = ".thanks-wrapper";
    $(formId).addClass('form-wrapper loading').append('<div class="loader"></div>');

    if ($('#ThanksPage').length > 0) {
        TransID = localStorage.getItem("TransactionId");
        $.ajax({

            type: 'post',
            url: '/postaspxfiles/PaymentForm/PaymentFinalizepost.aspx?Transactionid=' + TransID + '&orderId=' + window.location.search.substring("1") + "&numberOFItems=" + numberOfItems,
            data: mydata,
            async: true,
            success: function (objData) {
                if (objData != null && objData != undefined) {
                    var data = JSON.parse(objData);
                    if (data.status == "success") {
                        $('#ThanksPageHeading').text("Thank You!");
                        $('#ThanksPageMsg').html('<p>Yay your booking is complete and we are excited to see you soon</p> '); //+ data.registrationResponseData.Transaction.Amount.Value

                        $('#SelectedTickets').html('<a href='+ window.location.origin + "/UaepTickets/" + localStorage.getItem("TransactionId") + '.pdf" class="ticketsnext-step btn btn-outline-green"  target="_blank"><span>Download Ticket</span></a>');

                        myTicketdata = { 'barcodeTicketItems': JSON.stringify(data.barcodeTicketItems) };
                        //$.ajax({
                        //    type: 'post',
                        //    url: '/postaspxfiles/PaymentForm/ImportTransactionApipost.aspx?Transactionid=' + TransID + '&orderId=' + window.location.search.substring("1") + '&numberOFItems=' + localStorage.getItem("numberOfItems"),
                        //    data: myTicketdata,
                        //    success: function (data) {
                        //        if (data != null && data.length > 0) {
                        //            if (data == "success") {

                        //                console.log("Import Transaction Api resturn Successful");
                        //            }
                        //        }
                        //    }, error: function (e) {
                        //        console.log(e);
                        //    }
                        //});


                    } else if (data.status == "InsufficientFund") {
                        $('#ThanksPageHeading').text("Insufficient Fund!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();

                    } else if (data.status == "DoNotHonor") {
                        $('#ThanksPageHeading').text("Honor Issue!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();

                    } else if (data.status == "IssuerInoperative") {
                        $('#ThanksPageHeading').text("Error!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();
                    } else if (data.status == "Cancel") {
                        $('#ThanksPageHeading').text("Cancelled!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Your Transaction has been cancelled.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();
                    }
                    else if (data == "fail") {
                        $('#ThanksPageHeading').text("Something Went Wrong!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();
                    }
                }

            }, error: function (e) {

                $('#ThanksPageHeading').text("Something Went Wrong!");
                $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green"  target="_blank"><span>Back to the Page</span></a>');
                console.log(e);
                $(formId).find('.loader').remove();
                sessionStorage.clear();

            }
        });
    }

    $("#loyaltyForm").wrap("<form  id='loyality-form' class='select-ticket-tab'></form>");
    $loyalityForm = $("#loyality-form");
    if ($loyalityForm.length > 0) {

        $('#loyaltyForm').contents().unwrap();
        function adjustIframeHeight() {
            var $body = $('body'),
                $iframe = $body.data('iframe.fv');
            if ($iframe) {
                $iframe.height($body.height());
            }
        }

        $loyalityForm
            .steps({
                headerTag: ".tickets-tab-steps-2",
                bodyTag: ".select-ticket-content",
                enablePagination: false,
                enableFinishButton: false,
                onStepChanged: function (e, currentIndex, priorIndex) {
                    adjustIframeHeight();
                },
                onStepChanging: function (e, currentIndex, newIndex) {
                    var fv = $loyalityForm.data('formValidation'),
                        $container = $loyalityForm.find('.select-ticket-content[data-step="' + currentIndex + '"]');
                    fv.validateContainer($container);
                    var isValidStep = fv.isValidContainer($container);
                    if (isValidStep === false || isValidStep === null) {
                        return false;
                    }
                    CardSessionStorage();
                    return true;
                },
                onFinishing: function (e, currentIndex) {
                    var fv = $loyalityForm.data('formValidation'),
                        $container = $loyalityForm.find('.select-ticket-content[data-step="' + currentIndex + '"]');
                    fv.validateContainer($container);
                    var isValidStep = fv.isValidContainer($container);
                    if (isValidStep === false || isValidStep === null) {
                        return false;
                    }
                    return true;
                },
                onFinished: function (event, currentIndex) {
                    console.log("Submitted!");
                }
            });


        $loyalityForm
            .formValidation({
                framework: 'bootstrap',
                // This option will not ignore invisible fields which belong to inactive panels
                excluded: ':disabled',
                fields: {
                    Fullname: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            },
                            regexp: {
                                regexp: letters,
                                message: ' '
                            }
                        }
                    },
                    email: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }, regexp: {
                                regexp: emailRegex,
                                message: ' '
                            }
                        }
                    },
                    number: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            },
                            regexp: { regexp: phone_numberRegex, message: ' ' }
                        }
                    },
                    date: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    //address: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: ' '
                    //        }
                    //    }
                    //},
                    TermsCheckBox: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    attachment: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    attachmentE: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    },
                    captcha: {
                        validators: {
                            notEmpty: {
                                message: ' '
                            }
                        }
                    }
                },
                onSuccess: function (e) {
                    e.preventDefault();
                    localStorage.setItem("itemNumber", $("input[name='radio']:checked").val());
                    var $form = $(e.target),
                        formId = '#' + $form[0].id;
                    //var imageupload = ($('#base64image').val()).serialize();
                    var data = $(formId).serialize() + "&telCode=" + $(".iti__selected-dial-code").text() + "&status=" + "Pending" + "&" + $("input[name='radio']:checked").data("key") + "=1&orderid=" + $('#orderid').val() + "&subtotal=" + $('#subtotal').data('subtotal') + "&totalamount=" + $('#totalamount').data('totalamount') + "&discount=" + $('#discount').data('discount') + "&ClubCode=" + localStorage.getItem("ClubCode") + "&picture=" + localStorage.getItem("base64Profile") + "&emiratesidpicture=" + localStorage.getItem("base64EmiratesId") + "&emiratesIdExtension=" + localStorage.getItem("base64EmiratesIdExtension") + "&pictureExtension=" + localStorage.getItem("base64ProfileExtension");
                    $(formId).addClass('loading').append('<div class="loader"></div>');
                    $.ajax({
                        type: 'post',
                        url: '/postaspxfiles/LoyalityForm/post.aspx ',
                        data: data,
                        success: function (objData) {
                            if (objData != null && objData != undefined) {
                                var data = JSON.parse(objData);
                                if (data.status == "success") {
                                    $("#paymentPortalUrlDiv").html("");
                                    var htmlPaymentPortal = "<form action='" + data.registrationResponseData.Transaction.PaymentPortal + "' method='post'>";
                                    htmlPaymentPortal += "<input type='Hidden' name='TransactionID' value='" + data.registrationResponseData.Transaction.TransactionID + "'/>";
                                    htmlPaymentPortal += "<p class='animate fadeInUp animated' data-animation='fadeInUp' data-duration='400'>Please click on the Button to Proceed With Payment</p>";
                                    htmlPaymentPortal += "<input type='submit' class='btn btn-outline-green btn-pay' value='Submit'>";
                                    htmlPaymentPortal += "</form>";
                                    localStorage.setItem("TransactionId", data.registrationResponseData.Transaction.TransactionID);

                                    $("#paymentPortalUrlDiv").append(htmlPaymentPortal);
                                    $(formId).removeClass('loading');
                                    $(formId).find('.loader').remove();
                                    $('#PaymentPortalButton').modal('show');
                                    $('#paymentButton').removeAttr("disabled");
                                    
                                } else if (data.status == "invalid") {
                                    $(formId).find('.loader').remove();
                                    $(formId).removeClass('loading');
                                    $("input[type=tel]").removeClass('error');
                                    $('#captchasdiv').removeClass('has-success');
                                    $('#captchasdiv').addClass('has-error');
                                    $(".errorCaptcha").show();
                                    setTimeout(function () {
                                        $(".errorCaptcha").hide();
                                        $(".error").hide();
                                        refresh();
                                    }, 4000);
                                } else if (data.status == "fail") {
                                    $(formId).find('.loader').remove();
                                    $(formId).removeClass('loading');
                                    $(".error").text();
                                    $(".error").show();
                                    setTimeout(function () {
                                        $(".errorCaptcha").hide();
                                        $(".error").hide();
                                        refresh();
                                    }, 4000);
                                }
                            }
                        },
                        error: function (e) {
                            $(formId).find('.loader').remove();
                            $(formId).data('formValidation').resetForm(true);
                            refresh();
                        }
                    });
                }
            });

        $(".loyal-prev-step").on("click", function (e) {
            e.preventDefault();
            $loyalityForm.steps("previous");
        });
        $(".loyal-next-step").on("click", function (e) {
            e.preventDefault();
            $loyalityForm.steps("next");
        });

    }

    if ($('.attachment-mn').length) {
        $('.attachmentUploadBtn').click(function () {
            $(this).siblings('.attachmentInput').trigger('click');
        });


        function formatBytes(bytes, decimals) {
            if (!decimals) decimals = 2;
            if (bytes === 0) return '0 Bytes';

            const k = 1024;
            const dm = decimals < 0 ? 0 : decimals;
            const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

            const i = Math.floor(Math.log(bytes) / Math.log(k));

            return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
        }
        $('.attachmentInput').change(function (e) {
            var thatvalue = $(this);
            var Extension = thatvalue.val().split('.').pop().toLowerCase();
            if (Extension == "gif" || Extension == "png" || Extension == "bmp"
                || Extension == "jpeg" || Extension == "jpg") {
                localStorage.setItem("base64ProfileExtension", Extension);
                var that = $(this);
                var files = e.target.files;
                for (var i = 0; i <= files.length; i++) {
                    if (i == files.length) {
                        break;
                    }
                    var file = files[i];
                    var reader = new FileReader();
                    reader.onload = (function (file) {
                        return function (event) {
                            var fileNameExt = file.name.split('.').pop();
                            localStorage.setItem("base64Profile", event.currentTarget.result.replace("data:image/jpeg;base64,", ""));//event.currentTarget.result.replace("data:image/jpeg;base64,", "");
                            var fileName = file.name.substring(0, file.name.indexOf('.'));
                            $(that).siblings('.multiple-file-preview').find('.attachments').remove();
                            $(that).siblings('.multiple-file-preview').find('ul').prepend('' +
                                '<li class="attachments notcompleted" data-order=0 data-id="' + file.lastModified + '"> ' +
                                '<span class="circleExt">' + fileNameExt + '</span>' +
                                '<div class="fileDetails"> ' +
                                '<h5><span>' + fileName + '</span></h5> ' +
                                '<span class="totalSize">' + formatBytes(file.size) + '</span> ' +
                                '<div class="progBar"> <span>Uploading</span> <span class="startLoadingLine"></span> <span class="totalLoadingLine"></span> </div> ' +
                                '</div>' +
                                '</li>' +
                                '');

                            $(that).siblings('.update-files').find('.remaining').text($(that).siblings('.multiple-file-preview').find('ul li.notcompleted').length);
                            var curProg = 0;
                            var interval = setInterval(function () {
                                curProg = curProg + 1;
                                $('.attachments[data-id="' + file.lastModified + '"] .progBar .totalLoadingLine').css('width', curProg + '%');
                                if (curProg > 100) clearInterval(interval);
                                if (curProg = 100) {
                                    setTimeout(function () {
                                        $('.attachments[data-id="' + file.lastModified + '"]').removeClass('notcompleted').addClass('completed');
                                        $('.attachments[data-id="' + file.lastModified + '"] .progBar').hide();
                                        $('.attachments[data-id="' + file.lastModified + '"] .totalSize').show();
                                        $(that).siblings('.update-files').find('.total').text($(that).siblings('.multiple-file-preview').find('ul li.completed').length);
                                        $(that).siblings('.update-files').find('.remaining').text($(that).siblings('.multiple-file-preview').find('ul li.notcompleted').length);
                                    }, 2200);
                                }

                            }, 1000);
                            scroll.update();
                        };
                    })(file);

                    reader.readAsDataURL(file);
                }

            }
            else if (Extension != 'gif' || Extension != 'png' || Extension != 'bmp' || Extension != 'jpeg' || Extension != 'jpg') {

                thatvalue.siblings('.text-danger').removeClass('d-none');
                setTimeout(function () { thatvalue.siblings('.text-danger').addClass('d-none'); }, 1000);

            }

        });
    }


    if ($('.attachment-mnE').length) {
        $('.attachmentUploadBtnE').click(function () {
            $(this).siblings('.attachmentInputE').trigger('click');
        });


        function formatBytes(bytes, decimals) {
            if (!decimals) decimals = 2;
            if (bytes === 0) return '0 Bytes';

            const k = 1024;
            const dm = decimals < 0 ? 0 : decimals;
            const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

            const i = Math.floor(Math.log(bytes) / Math.log(k));

            return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
        }
        $('.attachmentInputE').change(function (e) {
            var thatvalue = $(this);
            var Extension = thatvalue.val().split('.').pop().toLowerCase();
            if (Extension == "gif" || Extension == "png" || Extension == "bmp"
                || Extension == "jpeg" || Extension == "jpg") {
                localStorage.setItem("base64EmiratesIdExtension", Extension);
                var that = $(this);
                var files = e.target.files;
                for (var i = 0; i <= files.length; i++) {
                    if (i == files.length) {
                        break;
                    }
                    var file = files[i];
                    var reader = new FileReader();
                    reader.onload = (function (file) {
                        return function (event) {
                            var fileNameExt = file.name.split('.').pop();
                            localStorage.setItem("base64EmiratesId", event.currentTarget.result.replace("data:image/jpeg;base64,", ""));//event.currentTarget.result.replace("data:image/jpeg;base64,", "");
                            var fileName = file.name.substring(0, file.name.indexOf('.'));
                            $(that).siblings('.multiple-file-preview').find('.attachmentsE').remove();
                            $(that).siblings('.multiple-file-preview').find('ul').prepend('' +
                                '<li class="attachmentsE notcompleted" data-order=0 data-id="' + file.lastModified + '"> ' +
                                '<span class="circleExt">' + fileNameExt + '</span>' +
                                '<div class="fileDetails"> ' +
                                '<h5><span>' + fileName + '</span></h5> ' +
                                '<span class="totalSize">' + formatBytes(file.size) + '</span> ' +
                                '<div class="progBar"> <span>Uploading</span> <span class="startLoadingLine"></span> <span class="totalLoadingLine"></span> </div> ' +
                                '</div>' +
                                '</li>' +
                                '');

                            $(that).siblings('.update-files').find('.remaining').text($(that).siblings('.multiple-file-preview').find('ul li.notcompleted').length);
                            var curProg = 0;
                            var interval = setInterval(function () {
                                curProg = curProg + 1;
                                $('.attachmentsE[data-id="' + file.lastModified + '"] .progBar .totalLoadingLine').css('width', curProg + '%');
                                if (curProg > 100) clearInterval(interval);
                                if (curProg = 100) {
                                    setTimeout(function () {
                                        $('.attachmentsE[data-id="' + file.lastModified + '"]').removeClass('notcompleted').addClass('completed');
                                        $('.attachmentsE[data-id="' + file.lastModified + '"] .progBar').hide();
                                        $('.attachmentsE[data-id="' + file.lastModified + '"] .totalSize').show();
                                        $(that).siblings('.update-files').find('.total').text($(that).siblings('.multiple-file-preview').find('ul li.completed').length);
                                        $(that).siblings('.update-files').find('.remaining').text($(that).siblings('.multiple-file-preview').find('ul li.notcompleted').length);
                                    }, 2200);
                                }

                            }, 1000);
                            scroll.update();
                        };
                    })(file);

                    reader.readAsDataURL(file);
                }

            }
            else if (Extension != 'gif' || Extension != 'png' || Extension != 'bmp' || Extension != 'jpeg' || Extension != 'jpg') {

                thatvalue.siblings('.text-danger').removeClass('d-none');
                setTimeout(function () { thatvalue.siblings('.text-danger').addClass('d-none'); }, 1000);

            }

        });
    }

    var TransID = "";
    var numberOfItems = 0;
    var ticketdata = [];
    var obj = {
        itemNo: localStorage.getItem("itemNumber"),
        qty: 1,
        KeyValue: localStorage.getItem("KeyValue")
    }
    ticketdata.push(obj);


    var mydata = { 'ticketdata': JSON.stringify(ticketdata) };
    if ($('#CardThanksPage').length > 0) {
        TransID = localStorage.getItem("TransactionId");
        $.ajax({

            type: 'post',
            url: '/postaspxfiles/LoyalityForm/PaymentFinalizepost.aspx?Transactionid=' + TransID + '&orderId=' + window.location.search.substring("1") + "&numberOFItems=1",
            data: mydata,
            success: function (objData) {
                if (objData != null && objData != undefined) {
                    var data = JSON.parse(objData);
                    if (data.status == "success") {
                        $('#ThanksPageHeading').text("Thank You!");
                        $('#ThanksPageMsg').html('<p>Your request has been submitted successfully!</p> '); //+ data.registrationResponseData.Transaction.Amount.Value
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');


                        $(formId).find('.loader').remove();
                        myTicketdata = { 'ticketdata': JSON.stringify(ticketdata) }; //{ 'barcodeTicketItems': JSON.stringify(data.barcodeTicketItems) };
                        //$.ajax({
                        //    type: 'post',
                        //    url: '/postaspxfiles/LoyalityForm/ImportTransactionApipost.aspx?Transactionid=' + TransID + '&orderId=' + window.location.search.substring("1") + '&numberOFItems=1&itemNo=' + localStorage.getItem("itemNumber") + '&KeyValue=' + localStorage.getItem("KeyValue"),
                        //    data: myTicketdata,
                        //    success: function (data) {
                        //        if (data != null && data.length > 0) {
                        //            if (data == "success") {

                        //                console.log("Import Transaction Api resturn Successful");
                        //            }
                        //        }
                        //    }, error: function (e) {
                        //        console.log(e);
                        //    }
                        //});


                    } else if (data.status == "InsufficientFund") {
                        $('#ThanksPageHeading').text("Insufficient Fund!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();

                    } else if (data.status == "DoNotHonor") {
                        $('#ThanksPageHeading').text("Honor Issue!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();

                    } else if (data.status == "IssuerInoperative") {
                        $('#ThanksPageHeading').text("Error!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();
                    } else if (data.status == "Cancel") {
                        $('#ThanksPageHeading').text("Cancelled!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Your Transaction has been cancelled.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();
                    }
                    else if (data == "fail") {
                        $('#ThanksPageHeading').text("Something Went Wrong!");
                        $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                        $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green" ><span>Back to the Page</span></a>');
                        sessionStorage.clear();
                        $(formId).find('.loader').remove();
                        refresh();
                    }
                }

            }, error: function (e) {

                $('#ThanksPageHeading').text("Something Went Wrong!");
                $('#ThanksPageMsg').html("<p class='mb - 0' id='ThanksPageMsg'>Sorry we are unable to process your payment, transaction is declined by card issuer.<br/>Kindly contact your card issuer for further assistance or use a new card and try again.</p>");
                $('#SelectedTickets').html('<a href="/Tickets-and-Loyalty-Programs" class="ticketsnext-step btn btn-outline-green"  target="_blank"><span>Back to the Page</span></a>');
                console.log(e);
                sessionStorage.clear();

            }
        });
    }


    if (($numbercounter.length) > 0) {
        var itemNo;
        $('.cardminus').click(function () {
            var $input = $(this).parent().find('input');
            var count = parseInt($input.val()) - 1;
            count = count < 0 ? 0 : count;
            $input.val(count);
            $input.change();
            itemNo = $(this).data('itemno');
            sessionStorage.clear();
            sessionStorage.setItem($input.attr("id"), $input.val() + ',' + itemNo);
            // localStorage.setItem($input.attr("id"), $('.unitprice').text());
            return false;

        });
        $('.minus').click(function () {
            var $input = $(this).parent().find('input');
            var count = parseInt($input.val()) - 1;
            count = count < 0 ? 0 : count;
            $input.val(count);
            $input.change();
            itemNo = $(this).data('itemno');
            sessionStorage.setItem($input.attr("id"), $input.val() + ',' + itemNo);
            // localStorage.setItem($input.attr("id"), $('.unitprice').text());
            return false;
        });

        $('.cardplus').click(function () {
            var $input = $(this).parent().find('input');

            if (BusinessLogicsMax() == false) {
                $('#MaxTicket').modal('show');
                return false;
            }

            $input.val(parseInt($input.val()) + 1);
            $input.change();
            itemNo = $(this).data('itemno');
            sessionStorage.clear();
            sessionStorage.setItem($input.attr("id"), $input.val() + ',' + itemNo);
            //localStorage.setItem($input.attr("id"), $('.unitprice').text());
            return false;
        });
        $('.plus').click(function () {
            var $input = $(this).parent().find('input');

            if (BusinessLogicsMax() == false) {
                $('#MaxTicket').modal('show');
                return false;
            }

            $input.val(parseInt($input.val()) + 1);
            $input.change();
            itemNo = $(this).data('itemno');
            sessionStorage.setItem($input.attr("id"), $input.val() + ',' + itemNo);
            //localStorage.setItem($input.attr("id"), $('.unitprice').text());
            return false;
        });

    }

    SessionStorage();

    $("#visitorfeedback").wrap("<form  id='feedback-form'></form>");
    if ($("#feedback-form").length > 0) {
        $("#feedback-form").formValidation({
            fields: {
                name: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },

                email: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }, regexp: {
                            regexp: emailRegex,
                            message: ' '
                        }
                    }
                },
                age: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: phone_numberRegex,
                            message: ' '
                        }
                    }
                },
                nationality: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        },
                        regexp: {
                            regexp: letters,
                            message: ' '
                        }
                    }
                },
                number: {
                    validators: {
                        regexp: { regexp: phone_numberRegex, message: ' ' }
                    }
                },
                captcha: {
                    validators: {
                        notEmpty: {
                            message: ' '
                        }
                    }
                }
            },
            onSuccess: function (e) {
                e.preventDefault();
                var $form = $(e.target),
                    formId = '#' + $form[0].id;
                var data = $(formId).serialize() + "&telCode=" + $(".iti__selected-dial-code").text();
                $(formId).addClass('loading').append('<div class="loader"></div>');
                $.ajax({
                    type: 'post',
                    url: '/postaspxfiles/VisitorsFeedback/post.aspx ',
                    data: data,
                    success: function (data) {
                        if (data == "success") {
                            setTimeout(function () {
                                var fields = $(formId).data('formValidation').getOptions().fields,
                                    $parent, $icon;
                                for (var field in fields) {
                                    $parent = $('[name="' + field + '"]').parents('.form-group');
                                }
                                // Then reset the form
                                $('.alert-success').addClass('in');
                                $(formId).data('formValidation').resetForm(true);
                                $("input[type=text], textarea").val("");
                                $('select[id=agee]').val(" ");
                                $('.selectpicker').selectpicker('refresh');
                                //$("input[type=checkbox]").removeAttr('checked');
                                $('.file-input-name').hide();
                                $('#visitorfeedback .thanks').show();
                                scroll.scrollTo('#visitorfeedback .thanks-inner');
                                $(formId).removeClass('loading');
                                $(formId).find('.loader').remove();
                                setTimeout(function () {
                                    $('#visitorfeedback .thanks').hide();
                                    $(formId).data('formValidation').resetForm(true);
                                    refresh();
                                    checkboxesValuesOnRefresh();
                                    scroll.scrollTo('.contact-form-sec');
                                }, 5000);
                            }, 1500);
                        } else if (data == "invalid") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            directionRtl();
                            $("input[type=tel]").removeClass('error');
                            $('#captchadiv').removeClass('has-success');
                            $('#captchadiv').addClass('has-error');
                            $(".errorCaptcha").show();
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                                checkboxesValuesOnRefresh();
                            }, 4000);
                        } else if (data == "fail") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            directionRtl();
                            $(".error").text();
                            $(".error").show();
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                                checkboxesValuesOnRefresh();
                            }, 4000);
                        } else if (data == "TimeError") {
                            $(formId).find('.loader').remove();
                            $(formId).removeClass('loading');
                            document.getElementById("TimeErrorId").style.direction = "rtl";
                            $('#captchadiv').addClass('has-error');
                            setTimeout(function () {
                                $(".errorCaptcha").hide();
                                $(".error").hide();
                                refresh();
                                checkboxesValuesOnRefresh();
                            }, 4000);
                        }
                    },
                    error: function (e) {
                        $(formId).find('.loader').remove();
                        $(formId).data('formValidation').resetForm(true);
                        refresh();
                        checkboxesValuesOnRefresh();
                    }
                });
            }
        });
    }
    if ($('#phone').length > 0) {
        var input = document.querySelector("#phone");

        var iti = window.intlTelInput(input, {
            nationalMode: false,
            separateDialCode: true,
            formatOnDisplay: true,
            rtl: true,
            initialCountry: "ae",
            utilsScript: "/assets/scripts/utils.js",
            autoPlaceholder: "aggressive",
            container: true
        });

        var reset = function () {
            input.classList.remove("error");
            //errorMsg.innerHTML = " ";
            //errorMsg.classList.add("hide");
        };

        // on blur: validate
        input.addEventListener('blur', function () {
            reset();
            if (input.value.trim()) {
                if (!iti.isValidNumber()) {
                    input.classList.add("error");
                    var errorCode = iti.getValidationError();
                    //errorMsg.innerHTML = errorMap[errorCode];
                    //errorMsg.classList.remove("hide");
                }
            }
        });

        input.addEventListener("open:countrydropdown", function () {
            $('.phone-field').addClass('highindex');
        });
        input.addEventListener("close:countrydropdown", function () {
            $('.phone-field').removeClass('highindex');
        });

        // on keyup / change flag: reset
        input.addEventListener('change', reset);
        input.addEventListener('keyup', reset);

    }

    if ($('#phone-2').length > 0) {
        var input = document.querySelector("#phone-2");

        var iti = window.intlTelInput(input, {
            nationalMode: false,
            separateDialCode: true,
            formatOnDisplay: true,
            initialCountry: "ae",
            utilsScript: "/assets/scripts/utils.js",
            autoPlaceholder: "aggressive",
            container: true
        });

        var reset = function () {
            input.classList.remove("error");
            //errorMsg.innerHTML = " ";
            //errorMsg.classList.add("hide");
        };

        // on blur: validate
        input.addEventListener('blur', function () {
            reset();
            if (input.value.trim()) {
                if (!iti.isValidNumber()) {
                    input.classList.add("error");
                    var errorCode = iti.getValidationError();
                    //errorMsg.innerHTML = errorMap[errorCode];
                    //errorMsg.classList.remove("hide");
                }
            }
        });
        // input.addEventListener('open', function (){
        //     $('.phone-field').addClass('highindex');
        // })
        input.addEventListener("open:countrydropdown", function () {
            $('.phone-field').addClass('highindex');
        });
        input.addEventListener("close:countrydropdown", function () {
            $('.phone-field').removeClass('highindex');
        });

        // on keyup / change flag: reset
        input.addEventListener('change', reset);
        input.addEventListener('keyup', reset);

    }

    if ($PressListSlider.length > 0) {
        $PressListSlider.slick({
            slidesToShow: 2,
            slidesToScroll: 1,
            infinite: false,
            rtl: true,
            prevArrow: '<a href="javascript:" class="slick-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="slick-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                    }
                }
            ]
        });
    }

    if ($PressThumbSlider.length > 0) {
        $PressThumbSlider.slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            infinite: false,
            rtl: true,
            prevArrow: '<a href="javascript:" class="slick-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="slick-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 992,
                    settings: {
                        slidesToShow: 2,
                    }
                },
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                    }
                }
            ]
        });
    }

    if ($MediaGallerySlider.length > 0) {
        $MediaGallerySlider.slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            infinite: true,
            rtl: true,
            centerMode: true,
            prevArrow: '<a href="javascript:" class="slick-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="slick-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 1199,
                    settings: {
                        slidesToShow: 1,
                    }
                },
            ]
        });
    }


    if ($SchTripSlider.length > 0) {
        $SchTripSlider.slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            infinite: false,
            rtl: true,
            centerMode: false,
            prevArrow: '<a href="javascript:" class="slick-arrow-left"></a>',
            nextArrow: '<a href="javascript:" class="slick-arrow-right"></a>',
            responsive: [
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                    }
                },
            ]
        });
    }


    if ($('#eventDatepicker').length > 0) {
        /*$(function () {
            $("#eventDatepicker").datepicker({});
        });*/
        var dateToday = new Date();
        $('#eventDatepicker').datepicker({
            isRTL: true,
            minDate: dateToday,
            maxDate: '+1y',
            beforeShow: function (input, inst) {
                scroll.stop();
                $('#ui-datepicker-div').removeClass(function () {
                    return $('input').get(0).id;
                });
                $('#ui-datepicker-div').addClass(this.id);
            },
            onSelect: function (dateText, inst) {
                scroll.start();
            },
            onClose: function (dateText, inst) {
                if ($("#bav-form").length) {
                    $("#bav-form").data('formValidation').revalidateField('date');
                }
            }
        });
    }

    if ($('#LoyaltyDatepicker').length > 0) {
        var dateToday = new Date();
        $('#LoyaltyDatepicker').datepicker({
            minDate: '-99y',//dateToday,
            maxDate: dateToday,
            changeMonth: true,
            changeYear: true,
            yearRange: "-100:+0",
            beforeShow: function (input, inst) {
                scroll.stop();
                $('#ui-datepicker-div').removeClass(function () {
                    return $('input').get(0).id;
                });
                $('#ui-datepicker-div').addClass(this.id);
            },
            onSelect: function (dateText, inst) {
                scroll.start();
            },
            onClose: function (dateText, inst) {
                if ($("#bav-form").length) {
                    $("#bav-form").data('formValidation').revalidateField('date');
                }

            }
        });
    }
    if ($(window).width() <= 767) {
        if ($('.tkt-nav-slider').length > 0) {
            $('.tkt-nav-slider').slick({
                dots: false,
                autoplay: false,
                infinite: false,
                rtl: true,
                speed: 1000,
                accessibility: false,
                slidesToShow: 1,
                slidesToScroll: 1,
                arrows: true,
                prevArrow: '<a href="javascript:" class="slick-arrow-left"></a>',
                nextArrow: '<a href="javascript:" class="slick-arrow-right"></a>',
            }).on('afterChange', function (event, slick, currentSlide, nextSlide) {
                console.log(currentSlide, slick);
                $('.tkt-nav-slider [data-slick-index="' + currentSlide + '"] .nav-link').click()
            });
        }
    }

    if ($(window).width() <= 767) {
        if ($('.book-nav-slider').length > 0) {
            $('.book-nav-slider').slick({
                dots: false,
                autoplay: false,
                infinite: false,
                speed: 1000,
                accessibility: false,
                rtl: true,
                slidesToShow: 1,
                slidesToScroll: 1,
                arrows: true,
                rtl: true,
                prevArrow: '<a href="javascript:" class="slick-arrow-right"></a>',
                nextArrow: '<a href="javascript:" class="slick-arrow-left"></a>',
            }).on('afterChange', function (event, slick, currentSlide, nextSlide) {
                console.log(currentSlide, slick);
                $('.book-nav-slider [data-slick-index="' + currentSlide + '"] .nav-link').click()
            });
        }
    }

    $(".tkt-nav-slider .nav-link").click(function () {

        $('.nav-link').removeClass("active");
        $('.nav-link').removeClass("show");
        //scroll.update();
        setTimeout(function () {
            scroll.update();
        }, 1000);
    });

    $(".book-nav-slider .nav-link").click(function () {

        $('.nav-link').removeClass("active");
        $('.nav-link').removeClass("show");

    });

    if ($("[data-fancybox]").length) {

        $("[data-fancybox]").fancybox({
            thumbs: false,
            hash: false,
            clickContent: false,
            infobar: false,
            buttons: [
                "close"
            ]
        });
        $.fancybox.defaults.backFocus = false;

    }


    $('#search').on('shown.bs.modal', function () {
        $('#textareaID').focus();
    });

    function openURL() {
        var name = document.getElementById('textareaID').value;
        var url = window.location.origin + '/ar/search.aspx?searchtext=' + name + '&searchmode=exactphrase';

        // In current window
        //window.location = url;
        // window.open(url);
        window.location.replace(url);
    }

    $('#icon-search').on("click", function (e) {
        e.preventDefault();
        openURL();
    });



    $(".dark_scroll").mCustomScrollbar({
        theme: "dark"
    });





    if ($('#eventDatepicker').length > 0) {
        $("#eventDatepicker").mCustomScrollbar({
            theme: "dark"
        });
    }



    $(document).on('click', '.fullCalModal .close, .modal-open', function () {
        scroll.start();
    });



    //    how offten do you visit park Question

    $(".visit-park").change(function () {
        $(".visit-park").prop('checked', false);
        $(this).prop('checked', true);
        $("#duraton").val("");
        $("#duraton").val($('.visit-park:checked').val());

    });

    //    would you receive-update Question
    $("#newsleter").val($('.receive-update:checked').val());

    $(".receive-update").change(function () {
        $(".receive-update").prop('checked', false);
        $(this).prop('checked', true);
        $("#newsleter").val("");
        $("#newsleter").val($('.receive-update:checked').val());
    });

    //    overall exp Question
    $("#overallexp").val($('.overall:checked').val());
    $(".overall").change(function () {
        $(".overall").prop('checked', false);
        $(this).prop('checked', true);
        $("#overallexp").val("");
        $("#overallexp").val($('.overall:checked').val());
    });

    //    cleanlines Question
    $("#clean").val($('.cleanliness:checked').val());
    $(".cleanliness").change(function () {
        $(".cleanliness").prop('checked', false);
        $(this).prop('checked', true);
        $("#clean").val("");
        $("#clean").val($('.cleanliness:checked').val());
    });

    //    value of moneye Question
    $("#valuemoney").val($('.money-check:checked').val());
    $(".money-check").change(function () {
        $(".money-check").prop('checked', false);
        $(this).prop('checked', true);
        $("#valuemoney").val("");
        $("#valuemoney").val($('.money-check:checked').val());
    });

    //    staff Question
    $("#staffprofessional").val($('.staff-check:checked').val());
    $(".staff-check").change(function () {
        $(".staff-check").prop('checked', false);
        $(this).prop('checked', true);
        $("#staffprofessional").val("");
        $("#staffprofessional").val($('.staff-check:checked').val());
    });

    //   dining exp Question
    $("#diningexp").val($('.dining-check:checked').val());
    $(".dining-check").change(function () {
        $(".dining-check").prop('checked', false);
        $(this).prop('checked', true);
        $("#diningexp").val("");
        $("#diningexp").val($('.dining-check:checked').val());
    });

    //    park Question
    $("#parkfacility").val($('.park-check:checked').val());
    $(".park-check").change(function () {
        $(".park-check").prop('checked', false);
        $(this).prop('checked', true);
        $("#parkfacility").val("");
        $("#parkfacility").val($('.park-check:checked').val());
    });

    //   operating Question
    $("#operatinghour").val($('.operating-check:checked').val());
    $(".operating-check").change(function () {
        $(".operating-check").prop('checked', false);
        $(this).prop('checked', true);
        $("#operatinghour").val("");
        $("#operatinghour").val($('.operating-check:checked').val());
    });

    //    event Question
    $("#eventsandactivity").val($('.events-check:checked').val());
    $(".events-check").change(function () {
        $(".events-check").prop('checked', false);
        $(this).prop('checked', true);
        $("#eventsandactivity").val("");
        $("#eventsandactivity").val($('.events-check:checked').val());
    });

    //    recommend-check Question

    $("#recomendation").val($('.recommend-check:checked').val());

    $(".recommend-check").change(function () {
        $(".recommend-check").prop('checked', false);
        $(this).prop('checked', true);
        $("#recomendation").val("");
        $("#recomendation").val($('.recommend-check:checked').val());
    });


    $('a').each(function () {
        if ($(this).prop('href') == window.location.href) {
            $(this).parents('li').addClass('active');
            $(this).removeClass('active');
            $(this).addClass('active');
        }
    });
});


$(window).on('load', function () {
    partnersSliderInit();
    downloadSliderInit();
    SelectIndex();
    //SizeInKb();
    setTimeout(function () { window.scrollTo(0, 0); }, 10);
    //if ($('.selectpicker').length) {
    //    $('.selectpicker').selectpicker({
    //        dropupAuto: false
    //    })
    //        .on('show.bs.select', function () {
    //            $(".select-time,.phone-field").removeClass("top-z-index");
    //            $(this).closest(".select-time,.phone-field").addClass("top-z-index");
    //        });
    //}
    var tl = gsap.timeline({ paused: true });
    setTimeout(function () {
        HomeSliderInit();
    }, 500)
    tl.fromTo(".loader-box", { opacity: 1 }, { opacity: 0, ease: "expo.out", duration: 1.5 })
    tl.fromTo(".loading-inner-bg", { height: '100%' }, { height: '0%', duration: 1 });
    tl.play().then(function () { $("#loader-wrapper").hide(); }).then(animateElements).then(function () {
        $('body').addClass('beforeLoaded');
        /*setTimeout(function () {
            $('.slidesControls').addClass('fadeInUp animated');
        }, 200)*/
        $('#content').removeAttr("style");
    }).then(iePopup);


    //animateElements();
    //scrollBarInit();

    calendarFunc();


    setTimeout(function () {
        window.dispatchEvent(new Event('resize'));
    }, 500)
    LocoScroll();
    if ($('.moveUp').length > 0) {
        var moveUp = new TimelineMax({ repeat: -1, yoyo: true, repeatDelay: 0.5 });
        moveUp.to(".moveUp", 3, { y: -13 });
        moveUp.to(".moveUp", 3, { y: 0 });
    }

    if ($('.wisdom-img-bx').length > 0) {
        //var rotateArc = gsap.timeline({repeat: -1});
        //TweenMax.to(".wisdom-img-arc", {rotation: 360, ease: Linear.easeNone, repeat: -1, duration: 100})
        //roundSvg.to(".rotating-right", {rotation: -360, ease: "none", duration: 50}, '-=50')
        //rotateArc.play();
    }


    /*var morphtl = gsap.timeline({repeat:-1, defaults: {duration: 3}}),
        morph = document.getElementById("morph-shape");

    morphtl.to(morph, {morphSVG:"#morph-shape1"}, "+=1")
        .to(morph, {morphSVG:"#morph-shape2"}, "+=1")
        .to(morph, {morphSVG:"#morph-shape3"}, "+=1")
        .to(morph, {morphSVG:morph}, "+=1");*/


    $("body").on("inview", "img[data-src]", function () {
        var $this = $(this);
        $this.attr("src", $this.attr("data-src"));
        // Remove it from the set of matching elements in order to avoid that the handler gets re-executed
        $this.removeAttr("data-src");
        $('.imgUnload').addClass('imgLoaded');
        setTimeout(function () {
            $(".imgLoaded").removeClass("imgUnload");
            $('.imgLoaded').find('.loader').remove();
        }, 10);
    });





    if ($(".swiper-container").length > 0) {

        var mySwiper = undefined;

        function initSwiper() {
            var screenWidth = $(window).width();
            if (screenWidth > 767 && mySwiper == undefined) {
                mySwiper = new Swiper('.swiper-container', {
                    // Optional parameters
                    direction: 'horizontal',
                    loop: false,
                    freeMode: true,
                    mousewheel: true,
                    //autoHeight: true, //enable auto height
                    slidesPerView: 'auto',
                    // If we need pagination

                    keyboard: {
                        enabled: true,
                    }
                });
            }
            else if (screenWidth < 768 && mySwiper != undefined) {
                mySwiper.destroy();
                mySwiper = undefined;
                $('.swiper-wrapper').removeAttr('style');
                $('.swiper-slide').removeAttr('style');
            }
        }

        //Swiper plugin initialization
        initSwiper();
    }

    function historySlider() {
        var historyCounter = 1;
        var historyMarginTop = 0;
        var totalSlides = $('.history-slider .history-data-counter').length;
        $('.history-data-counter').each(function (index) {
            $(this).attr("data-count", index + 1);
        });

        function arrowDisableClass() {
            if (historyCounter >= totalSlides) {
                $(".history-next").addClass("history-arrow-disable");
            } else {
                $(".history-next").removeClass("history-arrow-disable");
            }
            if (historyCounter <= 1) {
                $(".history-prev").addClass("history-arrow-disable");
            } else {
                $(".history-prev").removeClass("history-arrow-disable");
            }
        }

        $('.history-slider [data-count]').removeClass("animate-slide");
        $('.history-slider [data-count="1"]').addClass("animate-slide");
        arrowDisableClass();
        $('.history-slider [data-count]').click(function (e) {
            e.preventDefault();
            var that = $(this);
            historyCounter = $(this).data("count");
            if (!$(this).hasClass("disable-click")) {
                $(this).addClass("disable-click");
                $(".history-arrow").addClass("disable-click");
                setTimeout(function () {
                    that.removeClass("disable-click");
                    $(".history-arrow").removeClass("disable-click");
                }, 1000);

                historyMarginTop = -$('.history-slider [data-count="' + (historyCounter) + '"]').outerHeight(true) * (historyCounter - 1);
                TweenLite.to(".history-slider", 1, { y: historyMarginTop });
                $('.history-slider [data-count]').removeClass("animate-slide");
                $('.history-slider [data-count="' + historyCounter + '"]').addClass("animate-slide");
            }
            arrowDisableClass();
        });
        $(".history-next").click(function (e) {
            e.preventDefault();
            if (historyCounter < totalSlides && !$(this).hasClass("disable-click")) {
                $(this).addClass("disable-click");
                setTimeout(function () {
                    $(".history-next").removeClass("disable-click");
                }, 1000);

                historyCounter++;
                historyMarginTop = historyMarginTop - $('.history-slider [data-count="' + (historyCounter) + '"]').outerHeight(true);
                TweenLite.to(".history-slider", 1, { y: historyMarginTop });
                $('.history-slider [data-count]').removeClass("animate-slide");
                $('.history-slider [data-count="' + historyCounter + '"]').addClass("animate-slide");
            }
            arrowDisableClass();
        })
        $(".history-prev").click(function (e) {
            e.preventDefault();
            if (historyCounter > 1 && !$(this).hasClass("disable-click")) {
                $(this).addClass("disable-click");
                setTimeout(function () {
                    $(".history-prev").removeClass("disable-click");
                }, 1000);

                historyCounter--;
                historyMarginTop = historyMarginTop + $('.history-slider [data-count="' + (historyCounter) + '"]').outerHeight(true);
                TweenLite.to(".history-slider", 1, { y: historyMarginTop });
                $('.history-slider [data-count]').removeClass("animate-slide");
                $('.history-slider [data-count="' + historyCounter + '"]').addClass("animate-slide");
            }
            arrowDisableClass();
        });

    }
    historySlider();
    calendarFunc();

});

$(document).keydown(function (event) {
    if (event.ctrlKey == true && (event.which == '61' || event.which == '107' || event.which == '173' || event.which == '109' || event.which == '187' || event.which == '189')) {
        event.preventDefault();
    }
});

$(window).bind('mousewheel DOMMouseScroll', function (event) {
    if (event.ctrlKey == true) {
        event.preventDefault();
    }
});


document.addEventListener('DOMContentLoaded', function () {
    /*
        var imgLoad0 = imagesLoaded( '.page-view', { background: true }, function() {
            console.log('page-view loaded');
        });
        imgLoad0.on( 'done', function( instance ) {
            new Core.Slider({

            });
        });
    */
});
