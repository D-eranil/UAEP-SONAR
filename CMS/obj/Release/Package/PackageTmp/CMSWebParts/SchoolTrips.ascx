<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_SchoolTrips" Codebehind="~/CMSWebParts/SchoolTrips.ascx.cs" %>
<input type="hidden" id="hdnNodePathTripType" runat="server" clientidmode="Static" />
<input type="hidden" id="hdnNodePathTripTime" runat="server" clientidmode="Static" />

<section class="contact-form-sec" data-scroll-section>
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
                        
                            <div id="school-form">

                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="rn" runat="server">Requester Name</label>
                                        <input type="text" class="form-control" id="rnp" runat="server" name="Rname" placeholder="Write requester Name" maxlength="50" clientidmode="Static">
                                    </div>
                                </div>
                                <div class="col-12 phone-field">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="rmn" runat="server" >Requester Mobile</label>
                                        <input type="tel" id="phone" class="form-control" name="number" placeholder="" maxlength="50">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="ema" runat="server">Email Address</label>
                                        <input type="text" class="form-control" name="email" id="em" runat="server" placeholder="Connect with us" maxlength="50" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="sc" runat="server" >School Name</label>
                                        <input type="text" class="form-control" name="Schname" id="scp" runat="server" placeholder="Write School Name" maxlength="50" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12 select-time">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="stt" runat="server" >School Trip Type</label>
                                        <select id="tripType" data-size="5" name="type" class="selectpicker">
                                            <option id="sttp" runat="server" ClientIDMode="Static" value=" ">Select type of trip</option>
                                            <%--<option value="0">Option1</option>
                                            <option value="1">Option2</option>
                                            <option value="2">Option3</option>
                                            <option value="3">Option4</option>
                                            <option value="4">Option5</option>--%>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="nos" runat="server">Number of Students</label>
                                        <input type="text" class="form-control" name="Numstd" id="nosp" runat="server" placeholder="Write number of students" maxlength="50" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="rd" runat="server">Requested Date</label>
                                        <input type="text" id="eventDatepicker" runat="server" name="date" autocomplete="off" placeholder="Select Date" class="form-control" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12 select-time">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="rt" runat="server" >Requested time</label>
                                        <select id="tripTime" data-size="5" name="time" class="selectpicker">
                                            <option id="rtp" runat="server" ClientIDMode="Static" value=" ">Select time</option>
                                            <%--<option value="0">12:00 am</option>
                                            <option value="1">1:00 am</option>
                                            <option value="2">2:00 am</option>
                                            <option value="3">3:00 am</option>
                                            <option value="4">4:00 am</option>
                                            <option value="5">5:00 am</option>
                                            <option value="6">6:00 am</option>
                                            <option value="7">7:00 am</option>
                                            <option value="8">8:00 am</option>
                                            <option value="9">9:00 am</option>
                                            <option value="10">10:00 am</option>
                                            <option value="11">11:00 am</option>
                                            <option value="12">12:00 pm</option>
                                            <option value="13">1:00 pm</option>
                                            <option value="14">2:00 pm</option>
                                            <option value="15">3:00 pm</option>
                                            <option value="16">4:00 pm</option>
                                            <option value="17">5:00 pm</option>
                                            <option value="18">6:00 pm</option>
                                            <option value="19">7:00 pm</option>
                                            <option value="20">8:00 pm</option>
                                            <option value="21">9:00 pm</option>
                                            <option value="22">10:00 pm</option>
                                            <option value="23">11:00 pm</option>--%>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="notes" runat="server" >Notes</label>
                                        <textarea class="form-control" name="message" id="notesp" runat="server" placeholder="Write description" maxlength="500" ClientIDMode="Static"></textarea>
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
                                        <span id="SchoolTripsCtaTitle" runat="server" clientidmode="Static">Submit</span>
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
    <div class="grid">
        <div class="container">
            <div class="row">
                <div class="col-3">
                    <div class="line-height">
                        <div class="grid-line"></div>
                    </div>
                </div>
                <div class="col-3 pl-0">
                    <div class="line-height">
                        <div class="grid-line"></div>
                    </div>
                </div>
                <div class="col-3 pl-0 text-right">
                    <div class="line-height">
                        <div class="grid-line"></div>
                        <div class="grid-line"></div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="line-height">
                        <div class="grid-line ml-auto"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
