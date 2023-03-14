<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_BookAVenueUaep" Codebehind="~/CMSWebParts/BookAVenueUaep.ascx.cs" %>

<input type="hidden" id="hdnNodePath" runat="server"  clientidmode="Static"/>
<input type="hidden" id="hdnNodePath1" runat="server"  clientidmode="Static"/>
<input type="hidden" id="hdnNodePath2" runat="server"  clientidmode="Static"/>
<input type="hidden" id="hdnNodePathBookingtype" runat="server"  clientidmode="Static"/>
<input type="hidden" id="hdnNodePathCorporate1" runat="server"  clientidmode="Static"/>
<input type="hidden" id="hdnNodePathCorporate2" runat="server"  clientidmode="Static"/>
<section class="contact-form-sec">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="contact-form">
                    <div class="contact-leaf-1">
                        <div data-scroll data-scroll-speed="-2" data-scroll-delay="0.05">
                            <img src="/assets/images/leaf-img-3.png" alt="" class="img-fluid moveUp">
                        </div>
                    </div>
                    <div class="contact-leaf-2">
                        <div data-scroll data-scroll-speed="-2" data-scroll-delay="0.05">
                            <img src="/assets/images/wisdom-leaf.png" alt="" class="img-fluid moveUp">
                        </div>
                    </div>
                    <div class="form-wrapper">

                        <div id="bavform">
                            <div class="row">

                                 <div class="col-12 select-field">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="Label2" runat="server" ClientIDMode="Static">Booking Type</label>
                                        <select id="idselection" name="BookingTypeOption" class="selectpicker">
                                            <option id="Label2Placeholder" runat="server" ClientIDMode="Static" value=" ">Select Booking Type</option>

                                            <option id="IndividualBooking" runat="server" ClientIDMode="Static" value="individual">Individual Booking</option>
                                            <option id="CorporateBooking" runat="server" ClientIDMode="Static" value="corporate">Corporate Booking</option>
                                            <%--<option value="Amphitheater">Amphitheater</option>
                                            <option value="Great Lawn">Great Lawn</option>--%>
                                        </select>
                                    </div>
                                </div>


                                <div class="col-12 select-field">
                                    <div id="voiindividualremove" class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="voi" runat="server">Venue of interest</label>
                                        <select id="query" name="question" class="selectpicker">
                                            <option id="queryplaceholder" runat="server" ClientIDMode="Static" value=" ">Select Venue</option>

                                            <%--<option value="Evening Garden">Evening Garden</option>
                                            <option value="Children's Garden">Children's Garden</option>
                                            <option value="Amphitheater">Amphitheater</option>
                                            <option value="Great Lawn">Great Lawn</option>--%>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12 select-field">
                                    <div  id="voicorpaorateremove" class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="voicorpaorate" runat="server">Venue of interest</label>
                                        <select id="query1" name="question" class="selectpicker">
                                            <option id="voicorpaoratePlaceholder" runat="server" ClientIDMode="Static" value=" ">Select Venue</option>
                                            
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="fn" runat="server">  </label> <!--First name-->
                                        <input type="text" class="form-control" name="Fname" id="fnp" runat="server" placeholder="Write your first name" maxlength="50" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="ln" runat="server">Last name</label>
                                        <input type="text" class="form-control" name="Lname" id="lnp" runat="server" placeholder="Write your last name" maxlength="50" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div id="cnremove"class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="cn" runat="server">Company name</label>
                                        <input type="text" class="form-control" name="Cname" id="cnam" runat="server" placeholder="Write your company name" maxlength="50" clientidmode="Static">
                                    </div>
                                </div>
                                <div class="col-12 phone-field">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="mn" runat="server">Mobile number</label>
                                        <input type="tel" id="phone" class="form-control" name="number" placeholder="" maxlength="50">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="ema" runat="server">Email</label>
                                        <input type="text" class="form-control" name="email" id="em" runat="server" placeholder="Connect with us" maxlength="50" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12 select-field">
                                    <div id="etindividualremove" class="form-group phone-field animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="et" runat="server">Event type</label>
                                        <select data-size="4" id="eventType" name="type" class="selectpicker">
                                            <option id="eventTypeplaceholder" runat="server" ClientIDMode="Static" value=" ">Select type of event</option>
                                            <%--<option value="Individual event">Individual event</option>
                                            <option value="Community group event">Community group event</option>
                                            <option value="Charity event">Charity event</option>
                                            <option value="Government event">Government event</option>
                                            <option value="Amphitheater event">Amphitheater event</option>
                                            <option value="Other">Other</option>--%>
                                        </select>
                                    </div>
                                </div>

                                 <div class="col-12 select-field">
                                    <div id="etcorporateremove" class="form-group phone-field animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="etcorporate" runat="server">Event type</label>
                                        <select data-size="4" id="eventTypeCorporate" name="type" class="selectpicker">
                                            <option id="eventTypeCorporateplaceholder" runat="server" ClientIDMode="Static" value=" ">Select type of event</option>
                                            <%--<option value="Individual event">Individual event</option>
                                            <option value="Community group event">Community group event</option>
                                            <option value="Charity event">Charity event</option>
                                            <option value="Government event">Government event</option>
                                            <option value="Amphitheater event">Amphitheater event</option>
                                            <option value="Other">Other</option>--%>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="ed" runat="server">Event date</label>
                                        <input maxlength="150" readonly='true' type="text" id="eventDatepicker" runat="server" name="date" autocomplete="off" placeholder="Select Date" class="form-control" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12 select-field">
                                    <div class="form-group animate" id="TimeDiv" data-animation="fadeInUp" data-duration="300">
                                        <label id="toe" runat="server">Select time</label>
                                        <select data-size="4" id="time" name="time" class="selectpicker">
                                            <option id="timeplaceholder" runat="server" ClientIDMode="Static" value=" ">Select time of event</option>
                                            <%--<option value="12:00 AM">12:00 am</option>
                                            <option value="01:00 AM">1:00 am</option>
                                            <option value="02:00 AM">2:00 am</option>
                                            <option value="03:00 AM">3:00 am</option>
                                            <option value="04:00 AM">4:00 am</option>
                                            <option value="05:00 AM">5:00 am</option>
                                            <option value="06:00 AM">6:00 am</option>
                                            <option value="07:00 AM">7:00 am</option>
                                            <option value="08:00 AM">8:00 am</option>
                                            <option value="09:00 AM">9:00 am</option>
                                            <option value="10:00 AM">10:00 am</option>
                                            <option value="11:00 AM">11:00 am</option>
                                            <option value="12:00 PM">12:00 pm</option>
                                            <option value="01:00 PM">1:00 pm</option>
                                            <option value="02:00 PM">2:00 pm</option>
                                            <option value="03:00 PM">3:00 pm</option>
                                            <option value="04:00 PM">4:00 pm</option>
                                            <option value="05:00 PM">5:00 pm</option>
                                            <option value="06:00 PM">6:00 pm</option>
                                            <option value="07:00 PM">7:00 pm</option>
                                            <option value="08:00 PM">8:00 pm</option>
                                            <option value="09:00 PM">9:00 pm</option>
                                            <option value="10:00 PM">10:00 pm</option>
                                            <option value="11:00 PM">11:00 pm</option>--%>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="des" runat="server">Description</label>
                                        <textarea class="form-control" name="message" id="mssg" runat="server" placeholder="Write description" maxlength="500" ClientIDMode="Static"></textarea>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6">
                                    <div class="captcha-box">
                                        <div class="captcha-img">
                                            <img id="imgCaptcha" src="/FormCaptcha.aspx" class="img-fluid" alt="">
                                        </div>
                                        <div class="captcha-field-box">
                                            <div class="form-group" id="captchadiv">
                                                <input type="text" id="txtcaptcha" class="form-control" runat="server" name="captcha" placeholder="Write code" maxlength="7" clientidmode="Static">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 text-left text-md-right">
                                    <button type="submit" class="btn btn-outline-green submit-btn animate" data-animation="fadeInUp" data-duration="300">
                                        <span id="BookaVenueCtaTitle" runat="server" clientidmode="Static">
                                        </span>
                                    </button>
                                </div>
                            </div>
                             <div class="thanks">
                            <div class="thanks-inner">
                                <h4 class="mb-1" id="tymsgBold" runat="server">Thanks For Contacting!</h4>
                                <p class="mb-0" id="tymsg" runat="server">One of our representatives will be in touch with you shortly.</p>
                            </div>
                        </div>
                            <div class="TimeError" id="TimeErrorId" style="display: none;">
                            <span style=" padding-top: 3px; color: red; font-size: 13px;" id="invalidtimemsg" runat="server" >Please Select a Valid Time</span>
                        </div>
                        <div class="error" id="errorId" style="display: none;">
                            <span style=" padding-top: 3px; color: red; font-size: 13px;" id="errormsg" runat="server">Something went wrong .Please submit again.</span>
                        </div>
                        <div class="errorCaptcha" id="errorCaptchaId" style="display: none;">
                            <span style=" padding-top: 3px; color: red; font-size: 13px;" id="invalidcapthchamsg" runat="server">Invalid Captcha.Please enter correct.</span>
                        </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    
</section>
