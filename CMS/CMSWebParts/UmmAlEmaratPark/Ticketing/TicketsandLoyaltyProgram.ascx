<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_UmmAlEmaratPark_Ticketing_TicketsandLoyaltyProgram" Codebehind="~/CMSWebParts/UmmAlEmaratPark/Ticketing/TicketsandLoyaltyProgram.ascx.cs" %>


    
<section class="ticket-tabs-sec inner-page-section tick-loyalty-page" data-scroll-section>
                <div class="container">
                           <div class="col-12 p-0">
                              <div class="tick-loyal-box">

                                 <div class="nav tkt-nav-slider animate" data-animation="fadeInUp" data-duration="400" id="myTab" role="tablist">
                                    <div>

                                    <a class="nav-item btn-link nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true"><i ><img src="/assets/svgs/entry-ticket.svg" alt="" class="img-fluid"></i><span id="firstTabTitle" runat="server" ClientIDMode="Static">Entry Tickets</span></a>

                                    </div>
                                    <div>

                                    <a class="nav-item btn-link nav-link" id="nav-profile-tab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false"><i><img src="/assets/svgs/loyalty-card.svg" alt="" class="img-fluid"></i><span id="secondTabTitle" runat="server" ClientIDMode="Static">Loyalty Programs</span></a>

                                    </div>
                                </div>

                                 <div class="tab-content" id="myTabContent">
                                    <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                                       <h4 class="animate" data-animation="fadeInUp" data-duration="400" id="firstTabSubTitle" runat="server" ClientIDMode="Static">You may purchase entry tickets at the park’s entrance OR avoid<br class="d-none d-lg-block"> the line by buying tickets online</h4>
                                       <div class="leaf-sep animate" data-animation="fadeInUp" data-duration="400">
                                          <img src="/assets/svgs/leaf-t.svg" alt="" class="img-fluid">
                                       </div>
                                       <div class="row mb-5">
                                          <div class="col-md-6" id="firstTabSummary" runat="server" ClientIDMode="Static" >
                                             
                                          
                                          </div>
                                          <div class="col-md-6">
                                             <div class="animate" data-animation="fadeInUp" data-duration="400">
                                                <img id="firstTabImage" runat="server" ClientIDMode="Static" src="/assets/images/ent-tick.jpg" class="img-fluid card-tick-img" alt=""/>
                                             </div>
                                          </div>
                                       </div>
                                       <div class="row">
                                           <div class="col-md-12 form-wrapper">
                                               <div id="paymentForm">
                                                
                                                    <div class="tickets-tab-steps">
                                                        <i>
                                                            <img src="/assets/svgs/ticket-green.svg" alt="" class="icon-black">
                                                            <img src="/assets/svgs/ticket-white.svg" alt="" class="icon-white">
                                                        </i>
                                                        <h5>Select Your <br>Ticket</h5>
                                                    </div>
                                                    <div class="select-ticket-content">
                                                        <h4 class="text-left mb-4">Select your ticket</h4>
                                                        <div class="select-ticket-table mb-5">
                                                            <table>
                                                                <thead>
                                                                    <tr>
                                                                        <th>Standard Ticket</th>
                                                                        <th>Price</th>
                                                                        <th>Quantity</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Adult</td>
                                                                        <td>AED 10.00</td>
                                                                        <td>
                                                                            <div class="number-counter">
                                                                                <span  class="minus">-</span>
                                                                                <input id="adult" type="text" value="0" disabled/>
                                                                                <span  class="plus">+</span>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Children</td>
                                                                        <td>AED 10.00</td>
                                                                        <td>
                                                                            <div class="number-counter">
                                                                                <span class="minus">-</span>
                                                                                <input id="children" type="text" value="0" disabled/>
                                                                                <span class="plus">+</span>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Special Abilities</td>
                                                                        <td>AED 10.00</td>
                                                                        <td>
                                                                            <div class="number-counter">
                                                                                <span class="minus">-</span>
                                                                                <input id="special" type="text" value="0" disabled/>
                                                                                <span class="plus">+</span>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <a href="javascript:" class="ticketsnext-step btn btn-outline-green animate" data-animation="fadeInUp" data-duration="400"><span>Get Price</span></a>
                                                    </div>
                                                    <div class="tickets-tab-steps">
                                                        <i>
                                                            <img src="/assets/svgs/file-green.svg" alt="" class="icon-black">
                                                            <img src="/assets/svgs/file-white.svg" alt="" class="icon-white">
                                                        </i>
                                                        <h5>Ticket <br>Summary</h5>
                                                    </div>
                                                    <div class="select-ticket-content">
                                                        <h4 class="text-left mb-4">Ticket Summary</h4>
                                                        <div class="select-ticket-table mb-5">
                                                            <table>
                                                                <thead>
                                                                    <tr>
                                                                        <th>Standard Ticket</th>
                                                                        <th>Price</th>
                                                                        <th>Quantity</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Adult</td>
                                                                        <td>AED 10.00</td>
                                                                        <td>
                                                                            <div class="quantity float-none float-md-right">
                                                                                <span>2x</span>
                                                                                <a class="change-btn" href="javascript:;">Change</a>
                                                                                <a class="del-btn" href="javascript:;"><img src="/assets/svgs/dustbin.svg" alt=""></a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Children</td>
                                                                        <td>AED 10.00</td>
                                                                        <td>
                                                                            <div class="quantity float-none float-md-right">
                                                                                <span>2x</span>
                                                                                <a class="change-btn" href="javascript:;">Change</a>
                                                                                <a class="del-btn" href="javascript:;"><img src="/assets/svgs/dustbin.svg" alt=""></a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Special Abilities</td>
                                                                        <td>AED 10.00</td>
                                                                        <td>
                                                                            <div class="quantity float-none float-md-right">
                                                                                <span>2x</span>
                                                                                <a class="change-btn" href="javascript:;">Change</a>
                                                                                <a class="del-btn" href="javascript:;"><img src="/assets/svgs/dustbin.svg" alt=""></a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="total-summary-points">
                                                            <ul>
                                                                
                                                                <li>
                                                                    <span>Total</span>
                                                                    <div class="dots"></div>
                                                                    <span>AED 46.00</span>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <a href="javascript:" class="ticketsnext-step btn btn-outline-green animate" data-animation="fadeInUp" data-duration="400"><span>Get Price</span></a>
                                                    </div>
                                                    <div class="tickets-tab-steps">
                                                        <i>
                                                            <img src="/assets/svgs/credit-green.svg" alt="" class="icon-black">
                                                            <img src="/assets/svgs/credit-white.svg" alt="" class="icon-white">
                                                        </i>
                                                        <h5>Payment <br>Method</h5>
                                                    </div>
                                                    <div class="select-ticket-content contact-form-sec p-0">
                                                        <h4 class="text-left mb-4">Payment Method</h4>
                                                        <div class="contact-form p-0">
                                                            <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                                                <label>Full name</label>
                                                                <input type="text" class="form-control" name="Fullname" placeholder="Write your full name">
                                                             </div>
                                                             <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                                                <label>Email address</label>
                                                                <input type="text" class="form-control" name="email" placeholder="Connect with us">
                                                             </div>
                                                             <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                                                <label>Mobile number</label>
                                                                <input type="tel" id="phone" class="form-control" name="number" placeholder="">
                                                             </div>
                                                             <div class="row">
                                                                <div class="col-md-6">
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
                                                                <div class="col-md-6 text-left text-md-right">
                                                                    <button type="submit" class="btn btn-outline-green submit-btn animate" data-animation="fadeInUp" data-duration="300">
                                                                         <span>
                                                                             Buy Now
                                                                         </span>
                                                                    </button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="thanks thanks-paymentmethod">
                                                            <div class="thanks-inner">
                                                                <h3 class="mb-1">Thank You!</h3>
                                                                <h4>We’ve successfully processed your payment of AED 46.00.</h4>
                                                                <ul class="mb-5">
                                                                    <li><span>Adult</span> 2x</li>
                                                                    <li><span>Children</span> 3x</li>
                                                                    <li><span>Special Abilities</span> 3x</li>
                                                                </ul>
                                                                <a href="javascript:" class="ticketsnext-step btn btn-outline-green animate" data-animation="fadeInUp" data-duration="400"><span>Download Ticket</span></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                              </div>
                                       </div>
                                    </div>

                                    <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                                       <h4 class="animate" data-animation="fadeInUp" data-duration="400" id="secondTabSubTitle" runat="server" ClientIDMode="Static">Currently, you can purchase one of our two loyalty cards to<br/> receive more value for your money. </h4>
                                       <div class="leaf-sep animate" data-animation="fadeInUp" data-duration="400">
                                          <img src="/assets/svgs/leaf-t.svg" alt="" class="img-fluid">
                                       </div>
                                       <div class="row">
                                          <div class="col-md-6" id="secondTabSummary" runat="server" ClientIDMode="Static" >
                                             
                                          </div>
                                          <div class="col-md-6">
                                             <div class="animate" data-animation="fadeInUp" data-duration="400">
                                                <img id="secondTabImage" runat="server" ClientIDMode="Static" src="/assets/images/loyal-card.jpg" class="img-fluid card-tick-img" alt=""/>
                                             </div>
                                          </div>
                                       </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <hr/>
                                            </div>
                                            <div class="col-md-12 form-wrapper">
                                                <div id="loyaltyForm">

                                                <%--<form action="javascript:" id="loyality-form" class="select-ticket-tab">--%>
                                                    <div class="tickets-tab-steps-2">
                                                        <i>
                                                            <img src="/assets/svgs/card-green.svg" alt="" class="icon-black">
                                                            <img src="/assets/svgs/card-white.svg" alt="" class="icon-white">
                                                        </i>
                                                        <h5>Select Your <br>Card</h5>
                                                    </div>
                                                    <div class="select-ticket-content">
                                                        <h4 class="text-left mb-4">Select your Card</h4>
                                                        <div class="select-ticket-table mb-5">
                                                            <table>
                                                                <thead>
                                                                <tr>
                                                                    <th>Cards</th>
                                                                    <th>Price</th>
                                                                    <th>Quantity</th>
                                                                </tr>
                                                                </thead>
                                                                <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <div class="radio-box">
                                                                            <label class="radio-btn">Silver Card
                                                                                <input type="radio" checked="checked" name="radio">
                                                                                <span class="checkmark"></span>
                                                                            </label>
                                                                        </div>
                                                                    </td>
                                                                    <td>AED 200.00</td>
                                                                    <td>
                                                                        <div class="number-counter">
                                                                            <span class="minus">-</span>
                                                                            <input type="text" value="0" disabled/>
                                                                            <span class="plus">+</span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><label class="radio-btn">Gold Card
                                                                        <input type="radio" name="radio">
                                                                        <span class="checkmark"></span>
                                                                    </label></td>
                                                                    <td>AED 499.00</td>
                                                                    <td>
                                                                        <div class="number-counter">
                                                                            <span class="minus">-</span>
                                                                            <input type="text" value="0" disabled/>
                                                                            <span class="plus">+</span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Enter Promo code if any</td>
                                                                    <td></td>
                                                                    <td><input type="text" value="2Zhb20y744"></td>
                                                                </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <a href="javascript:" class="loyal-next-step btn btn-outline-green animate" data-animation="fadeInUp" data-duration="400"><span>Get Price</span></a>
                                                    </div>
                                                    <div class="tickets-tab-steps-2">
                                                        <i>
                                                            <img src="/assets/svgs/file-green.svg" alt="" class="icon-black">
                                                            <img src="/assets/svgs/file-white.svg" alt="" class="icon-white">
                                                        </i>
                                                        <h5>Card <br>Summary</h5>
                                                    </div>
                                                    <div class="select-ticket-content">
                                                        <h4 class="text-left mb-4">Card Summary</h4>
                                                        <div class="select-ticket-table mb-5">
                                                            <table>
                                                                <thead>
                                                                <tr>
                                                                    <th>Cards</th>
                                                                    <th>Price</th>
                                                                    <th>Quantity</th>
                                                                </tr>
                                                                </thead>
                                                                <tbody>

                                                                <tr>
                                                                    <td>Silver Card</td>
                                                                    <td>AED 200.00</td>
                                                                    <td>
                                                                        <div class="quantity float-none float-md-right">
                                                                            <span>2x</span>
                                                                            <a class="change-btn" href="javascript:;">Change</a>
                                                                            <a class="del-btn" href="javascript:;"><img src="/assets/svgs/dustbin.svg" alt=""></a>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Promo code added</td>
                                                                    <td></td>
                                                                    <td>**********</td>
                                                                </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="total-summary-points">
                                                            <ul>
                                                                <li>
                                                                    <span>Subtotal</span>
                                                                    <div class="dots"></div>
                                                                    <span>AED 50.00</span>
                                                                </li>
                                                                <li>
                                                                    <span>Discount Code</span>
                                                                    <div class="dots"></div>
                                                                    <span>AED -10.00</span>
                                                                </li>
                                                                <li>
                                                                    <span>Tax</span>
                                                                    <div class="dots"></div>
                                                                    <span>AED 6.00</span>
                                                                </li>
                                                                <li>
                                                                    <span>Total</span>
                                                                    <div class="dots"></div>
                                                                    <span>AED 46.00</span>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <a href="javascript:" class="loyal-next-step btn btn-outline-green animate" data-animation="fadeInUp" data-duration="400"><span>Get Price</span></a>
                                                    </div>
                                                    <div class="tickets-tab-steps-2">
                                                        <i>
                                                            <img src="/assets/svgs/credit-green.svg" alt="" class="icon-black">
                                                            <img src="/assets/svgs/credit-white.svg" alt="" class="icon-white">
                                                        </i>
                                                        <h5>Payment <br>Method</h5>
                                                    </div>
                                                    <div class="select-ticket-content contact-form-sec p-0">
                                                        <h4 class="text-left mb-4">Payment Method</h4>
                                                        <div class="contact-form p-0">
                                                            <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                                                <label>Full name</label>
                                                                <input type="text" class="form-control" name="Fullname" placeholder="Write your full name">
                                                            </div>
                                                            <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                                                <label>Email address</label>
                                                                <input type="text" class="form-control" name="email" placeholder="Connect with us">
                                                            </div>
                                                            <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                                                <label>Mobile number</label>
                                                                <input type="tel" id="phone-2" class="form-control" name="number" placeholder="">
                                                            </div>
                                                            <div class="form-group animate" data-animation="fadeInUp" data-duration="300">
                                                                <label>Address</label>
                                                                <input type="tel" id="address" class="form-control" name="number" placeholder="">

                                                            </div>
                                                            <div class="form-group border-0">
                                                                <p class="mb-0">Please collect your Loyalty card from the park ticket counter</p>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-6">
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
                                                                <div class="col-md-6 text-left text-md-right">
                                                                    <button type="submit" class="btn btn-outline-green submit-btn animate" data-animation="fadeInUp" data-duration="300">
                                                                         <span>
                                                                             Buy Now
                                                                         </span>
                                                                    </button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="thanks thanks-paymentmethod">
                                                            <div class="thanks-inner">
                                                                <h3 class="mb-1">Thank You!</h3>
                                                                <h4>We’ve successfully processed your payment of AED 46.00.</h4>
                                                                <ul class="mb-5">
                                                                    <li><span>Silver Card</span> 1x</li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                 </div>
                                 <div class="leaf-three">
                                    <div  data-scroll data-scroll-speed="-1" data-scroll-delay="0.05">
                                       <img src="/assets/images/leaf-img-3.png" alt="" class="img-fluid moveUp">
                                    </div>
                                 </div>
                              </div>
                           </div>
                        </div>
            </section>