<%@ Control Language="C#" AutoEventWireup="True" Inherits="CMSWebParts_Umm_Al_Emarat_Park_ContactUsUaep" CodeBehind="~/CMSWebParts/Umm Al Emarat Park/ContactUsUaep.ascx.cs" %>

<section class="contact-form-sec" data-scroll-section>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h3 class="animate" id="formTitle" runat="server" data-animation="fadeInUp" data-duration="300"></h3>
                    </div>
                    <div class="col-12">
                        <div class="contact-form">
                            <div class="contact-leaf-1">
                                <div  data-scroll data-scroll-speed="-2" data-scroll-delay="0.05">
                                    <img src="/assets/images/leaf-img-3.png" alt="" class="img-fluid moveUp">
                                </div>
                            </div>

                            <div class="contact-leaf-2">
                                <div  data-scroll data-scroll-speed="-2" data-scroll-delay="0.05">
                                    <img src="/assets/images/wisdom-leaf.png" alt="" class="img-fluid moveUp">
                                </div>
                            </div>
                            <div class="form-wrapper">
                                <form action="javascript:" id="cnt-form">
                                    <div class="row">
                                     <div class="col-12">
                                        <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                            <label>First name</label>
                                            <input type="text" class="form-control" name="Fname" placeholder="Write your first name">
                                         </div>
                                     </div>
                                     <div class="col-12">
                                        <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                             <label>Last name</label>
                                             <input type="text" class="form-control" name="Lname" placeholder="Write your last name">
                                         </div>
                                     </div>
                                     <div class="col-12">
                                        <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                           <label>Email address</label>
                                           <input type="text" class="form-control" name="email" placeholder="Connect with us">
                                        </div>
                                     </div>
                                     <div class="col-12 phone-field">
                                        <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                           <label>Mobile number</label>
                                           <input type="tel" id="phone" class="form-control" name="number" placeholder="">
                                        </div>
                                     </div>
                                     <div class="col-12">
                                        <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                           <label>City</label>
                                           <input type="tel" class="form-control" name="city" placeholder="Where are you from?">
                                        </div>
                                     </div>
                                     <div class="col-12 phone-field">
                                        <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                           <label>Question regarding</label>
                                           <select name="question" class="selectpicker">
                                               <option value=" ">Where are you from?</option>
                                               <option value="Subject One">Subject One</option>
                                               <option value="Subject Two">Subject Two</option>
                                           </select>
                                        </div>
                                     </div>
                                     <div class="col-12">
                                        <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                         <label>Your message</label>
                                         <textarea class="form-control" name="message" placeholder="Where are you from?"></textarea>
                                      </div>
                                     </div>
                                     <div class="col-12 col-md-6">
                                        <div class="captcha-box">
                                            <div class="captcha-img">
                                                <img src="/assets/images/captcha-img.jpg" class="img-fluid" alt="">
                                            </div>
                                            <div class="captcha-field-box">
                                                <div class="form-group">
                                                <input type="text" class="form-control" name="captcha" placeholder="Write code">
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                     <div class="col-12 col-md-6 text-left text-md-right">
                                            <button type="submit" class="btn btn-outline-green submit-btn animate" data-animation="fadeInUp" data-duration="300">
                                                <span>
                                                    Submit
                                                </span>
                                            </button>
                                        </div>
                                    </div>
                                </form>
                                <div class="thanks">
                                    <div class="thanks-inner">
                                        <h4 class="mb-1">Thanks For Contacting!</h4>
                                        <p class="mb-0">One of our representatives will be in touch with you shortly.</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div><div class="grid">
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
