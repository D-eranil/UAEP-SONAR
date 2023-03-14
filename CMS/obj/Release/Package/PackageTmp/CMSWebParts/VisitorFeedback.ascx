<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_VisitorFeedback" Codebehind="~/CMSWebParts/VisitorFeedback.ascx.cs" %>
<%@ Import Namespace="CMS.DocumentEngine" %>
<input type="hidden" id="hdnNodePathAge" runat="server"  clientidmode="Static"/>
<section class="contact-form-sec visitor-feed-form-sec" data-scroll-section>
    <div class="container">
        <div class="row">
            <div class="col-12">
                <h4 class="animate" data-animation="fadeInUp" data-duration="300" id="formTitle" runat="server">Dear Valued Visitor,</h4>
                <p class="animate" data-animation="fadeInUp" data-duration="300" id="formSubTitle" runat="server">
                    Thank you for visiting Umm Al Emarat Park. It is our great pleasure to provide you the best quality of service at all times.
                    <br>
                    Your assistance in completing this form is greatly appreciated, and will help in improving our services and facilities.
                </p>
            </div>
            <div class="col-12">
                <div class="contact-form feedback-form">
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
                        <div id="visitorfeedback">

                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="nam" runat="server">Name</label>
                                        <input type="text" class="form-control" name="name" id="np" runat="server" placeholder="Write your name" maxlength="50" ClientIDMode="Static">
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="emai" runat="server">Email address</label>
                                        <input type="text" class="form-control" name="email" id="em" runat="server" placeholder="Connect with us" maxlength="50" clientidmode="Static">
                                    </div>
                                </div>

                                <div class="col-12 selectpickr-mn">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="ag" runat="server">Age</label>
                                        <input type="text" class="form-control" name="age" id="ageplaceholder" runat="server" placeholder="Write your age" maxlength="3" clientidmode="Static">
                                        <%--<select id="agee" name="age" class="selectpicker">
                                            <option id="ageplaceholder" runat="server" ClientIDMode="Static" value=" ">Select your age</option>
                                            <option value="18">18</option>
                                            <option value="19">19</option>
                                             <option value="20">20</option>
                                             <option value="21">21</option>
                                             <option value="22">22</option>
                                             <option value="23">23</option>
                                             <option value="24">24</option>
                                             <option value="25">25</option>
                                             <option value="26">26</option>
                                        </select>--%>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="nationalty" runat="server">Nationality</label>
                                        <input type="text" class="form-control" name="nationality" id="nation" runat="server"  placeholder="Write your nationality" maxlength="50" ClientIDMode="Static">
                                    </div>
                                </div>

                                <div class="col-12 phone-field">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="mn" runat="server">Mobile number</label>
                                        <input type="tel" id="phone" class="form-control" name="number" placeholder="" maxlength="50">
                                    </div>
                                </div>


                                <div class="col-12">
                                    <div class="form-group visit-q1 border-bottom-0 animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="duration" runat="server">How often do you
                                            <br>
                                            visit the park?</label>
                                        <div class="radio-form">
                                            <div class="radio-box">
                                                <label class="container-checkbox">
                                                    <input type="checkbox" class="visit-park" Value="Daily"/>
                                                    <span class="checkmark"></span>
                                                    <strong id="daily" runat="server" >Daily</strong>
                                                </label>
                                            </div>
                                            <div class="radio-box">
                                                <label class="container-checkbox">
                                                    <input type="checkbox" class="visit-park" checked Value="Weekly" />
                                                    <span class="checkmark"></span>
                                                    <strong id="weekly" runat="server">Weekly</strong>
                                                </label>
                                            </div>
                                            <div class="radio-box">
                                                <label class="container-checkbox">
                                                    <input type="checkbox" class="visit-park" Value="Biweekly"/>
                                                    <span class="checkmark"></span>
                                                    <strong id="biweekly" runat="server">Biweekly</strong>
                                                </label>
                                            </div>
                                            <div class="radio-box">
                                                <label class="container-checkbox">
                                                    <input type="checkbox" class="visit-park" Value="Monthly" />
                                                    <span class="checkmark"></span>
                                                    <strong id="monthly" runat="server">Monthly</strong>
                                                </label>
                                            </div>
                                        </div>
                                        <input type="text" id="duraton" name="duration" style="display:none;" />
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group visit-q2 border-bottom-0 animate" data-animation="fadeInUp" data-duration="300">
                                        <div class="park-update-qs">
                                            <label id="nl" runat="server">
                                                Would you like to receive updates and
                                                            news about the Park on your email?
                                            </label>

                                            <div class="radio-form">
                                                <div class="radio-box">
                                                    <label class="container-checkbox">
                                                        <input type="checkbox" checked class="receive-update" value="Yes"/>
                                                        <span class="checkmark"></span>
                                                        <strong id="yes" runat="server">Yes</strong>
                                                    </label>
                                                </div>
                                                <div class="radio-box">
                                                    <label class="container-checkbox">
                                                        <input type="checkbox" class="receive-update" value="No" />
                                                        <span class="checkmark"></span>
                                                        <strong id="no" runat="server">No</strong>
                                                    </label>
                                                </div>
                                            </div>
                                            <input type="text" id="newsleter" name="newslettr" style="display:none;" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group visit-q3 border-bottom-0 animate" data-animation="fadeInUp" data-duration="300">
                                        <div class="table-responsive">
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th scope="col" colspan="3"></th>

                                                        <th scope="col" id="excellent" runat="server">Excellent</th>
                                                        <th scope="col" id="good" runat="server">Good</th>
                                                        <th scope="col" id="fair" runat="server">Fair</th>
                                                        <th scope="col" id="poor" runat="server">Poor</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td scope="col" colspan="3" id="oe" runat="server" >Overall experience</td>
                                                        
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" checked class="overall" value="Excellent"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="overall" value="Good"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="overall" value="Fair"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="overall" value="Poor" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                            <input type="text" id="overallexp" name="overallexperience" style="display:none;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td scope="col" colspan="3" id="cle" runat="server">Cleanliness</td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="cleanliness" value="Excellent"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" checked class="cleanliness" value="Good" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="cleanliness" value="Fair"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="cleanliness" value="Poor" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                            <input type="text" id="clean" name="cleanlines" style="display:none;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td scope="col" colspan="3" id="vfm" runat="server">Value for money</td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="money-check" value="Excellent"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" checked class="money-check" value="Good" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="money-check" value="Fair" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="money-check" value="Poor"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>                                                            
                                                            <input type="text" id="valuemoney" name="valueformoney" style="display:none;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td scope="col" colspan="3" id="spaa" runat="server">Staff’s professionalism and aptitude</td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="staff-check" value="Excellent"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" checked class="staff-check" value="Good" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="staff-check" value="Fair"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="staff-check" value="Poor"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                            <input type="text" id="staffprofessional" name="staffprofessionalism" style="display:none;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td scope="col" colspan="3" id="de" runat="server">Dining experience</td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="dining-check" value="Excellent"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="dining-check" value="Good"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" checked class="dining-check" value="Fair" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="dining-check" value="Poor" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                            <input type="text" id="diningexp" name="diningexperience" style="display:none;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td scope="col" colspan="3" id="pf" runat="server">Park facilities</td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="park-check" value="Excellent" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" checked class="park-check" value="Good"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="park-check" value="Fair" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="park-check" value="Poor" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                            <input type="text" id="parkfacility" name="parkfacilities" style="display:none;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td scope="col" colspan="3" id="oh" runat="server">Operating hours</td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="operating-check" value="Excellent"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="operating-check" value="Good"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" checked class="operating-check" value="Fair" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="operating-check" value="Poor" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                            <input type="text" id="operatinghour" name="operatinghours" style="display:none;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td scope="col" colspan="3" id="eaa" runat="server">Events and activities</td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="events-check" value="Excellent"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" checked class="events-check" value="Good"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="events-check" value="Fair" />
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                        </td>

                                                        <td scope="col">
                                                            <div class="radio-box">
                                                                <label class="container-checkbox">
                                                                    <input type="checkbox" class="events-check" value="Poor"/>
                                                                    <span class="checkmark"></span>

                                                                </label>
                                                            </div>
                                                            <input type="text" id="eventsandactivity" name="eventsandactivities" style="display:none;" />
                                                        </td>
                                                    </tr>
                                                </tbody>

                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group visit-q4 animate" data-animation="fadeInUp" data-duration="300">
                                        <div class="park-update-qs">
                                            <label id="recommend" runat="server">
                                                Would you recommend
                                                <br>
                                                Umm Al Emarat Park to others?
                                            </label>

                                            <div class="radio-form">
                                                <div class="radio-box">
                                                    <label class="container-checkbox">
                                                        <input type="checkbox" checked class="recommend-check" value="Yes" />
                                                        <span class="checkmark"></span>
                                                        <strong id="ryes" runat="server">Yes</strong>
                                                    </label>
                                                </div>
                                                <div class="radio-box">
                                                    <label class="container-checkbox">
                                                        <input type="checkbox" class="recommend-check" value ="No"/>
                                                        <span class="checkmark"></span>
                                                        <strong id="rno" runat="server">No</strong>
                                                    </label>
                                                </div>
                                            </div>
                                            <input type="text" id="recomendation" name="recommendation" style="display:none;" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                        <label id="msg" runat="server">Comments</label>
                                        <textarea class="form-control" name="comments" id="cmnts" runat="server" placeholder="Write your comments" maxlength="500" clientidmode="Static"></textarea>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6">
                                    <div class="captcha-box">
                                        <div class="captcha-img">
                                            <img src="/FormCaptcha.aspx" id="imgCaptcha" class="img-fluid" alt="">
                                        </div>
                                        <div class="captcha-field-box">
                                            <div class="form-group" id="captchadiv">
                                                <input type="text" id="txtcaptcha" runat="server" class="form-control" name="captcha" placeholder="Write code" maxlength="7" clientidmode="Static">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 text-left text-md-right">
                                    <button type="submit" class="btn btn-outline-green submit-btn animate" data-animation="fadeInUp" data-duration="300">
                                        <span id="VistorFeedbackCtaTitle" runat="server" clientidmode="Static">Submit</span>
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

