<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_ContactUsUaep" Codebehind="~/CMSWebParts/ContactUsUaep.ascx.cs" %>
<%@ Import Namespace="CMS.DocumentEngine" %>

<input type="hidden" id="hdnNodePath" runat="server"  clientidmode="Static"/>
<section class="contact-form-sec" data-scroll-section>
    <div class="container">
        <div class="row">
            <div class="col-12">
                <h3 class="animate" id="formTitle" runat="server" data-animation="fadeInUp" data-duration="300"></h3>
            </div>
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

                        <div id="contactform">

                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="fn" runat="server">First Name</label>
                                        <input type="text" class="form-control" name="Fname" id="fnp" runat="server" placeholder="Write your first name" maxlength="50" clientidmode="Static">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="ln" runat="server">Last name</label>
                                        <input type="text" class="form-control" name="Lname" id="lnp" runat="server" placeholder="Write your last name" maxlength="50" clientidmode="Static">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="emai" runat="server">Email address</label>
                                        <input type="text" class="form-control" name="email" id="em" runat="server" placeholder="Connect with us" maxlength="50" clientidmode="Static">
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
                                        <label id="cty" runat="server">City</label>
                                        <input type="text" class="form-control" name="city" id="cit" runat="server" placeholder="Where are you from?" maxlength="50" clientidmode="Static">
                                    </div>
                                </div>
                                <div class="col-12 selectpickr-mn ">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="qr" runat="server">Question regarding</label>

                                        <select id="query" name="question" class="selectpicker">
                                            <option id="questionplaceholder" runat="server" ClientIDMode="Static" value="">Question Regarding</option>
                                            <%--<option value="Attraction">Attraction</option>
                                            <option value="Events">Events</option>
                                            <option value="Operations">Operations</option>
                                            <option value="Media">Media</option>
                                            <option value="Others">Others</option>--%>
                                        </select>

                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="msg" runat="server">Your message</label>
                                        <textarea class="form-control" name="message" id="mssg" runat="server" placeholder="Where are you from?" maxlength="500" clientidmode="Static"></textarea>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6">
                                    <div class="captcha-box">
                                        <div class="captcha-img">

                                            <img id="imgCaptcha" class="img-fluid" src="/FormCaptcha.aspx">
                                        </div>
                                        <div class="captcha-field-box">
                                            <div class="form-group" id="captchadiv">
                                                <input type="text" id="txtcaptcha" class="form-control" name="captcha" runat="server" placeholder="Write code" maxlength="7" clientidmode="Static">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 text-left text-md-right">
                                    <button type="submit" class="btn btn-outline-green submit-btn animate" data-animation="fadeInUp" data-duration="300">
                                        <span id="ContactUsCtaTitle" runat="server" clientidmode="Static"></span>
                                    </button>
                                </div>

                            </div>
                            <div class="thanks">
                                <div class="thanks-inner">
                                    <h4 class="mb-1" id="tymsgBold" runat="server">Thanks For Contacting!</h4>
                                    <p class="mb-0" id="tymsg" runat="server">One of our representatives will be in touch with you shortly.</p>
                                </div>
                            </div>
                            <div class="error" id="errorId" style="display: none;">
                                <span style="padding-top: 3px; color: red; font-size: 13px;" id="errormsg" runat="server">Something went wrong .Please submit again.</span>
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
