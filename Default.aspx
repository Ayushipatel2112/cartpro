<%@ Page Title="Home - CartPro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CartProWebApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CartPro - Fashion & Style Collection
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Start Hero Section -->
    <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>Fashion &amp; Style <span class="d-block"></span>Collection</h1>
                        <p class="mb-4">Discover our curated selection of premium shoes, perfumes, belts, and watches to elevate your style and complement your modern lifestyle.</p>
                        <p><a href="Shop.aspx" class="btn btn-secondary me-2">Shop Now</a><a href="#" class="btn btn-white-outline">Explore</a></p>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="hero-img-wrap">
                        <img src="images/couch.png" class="img-fluid" style="width: 90%; margin-left: 15%;" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Hero Section -->
    <!-- Start Product Section -->
    <div class="product-section">
        <div class="container">
            <div class="row">
                <!-- Start Column 1 -->
                <div class="col-md-12 col-lg-3 mb-5 mb-lg-0">
                    <h2 class="mb-4 section-title">Explore Our Collections</h2>
                    <p class="mb-4">From timeless watches to elegant shoes, our collections are crafted to perfection. Discover accessories that define your style.</p>
                    <p><a href="Shop.aspx" class="btn">Explore</a></p>
                </div>
                <!-- End Column 1 -->
                <!-- Start Column 2 -->
                <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                    <a class="product-item" href="Cart.aspx">
                        <img src="images/product-1.png" class="img-fluid product-thumbnail" />
                        <h3 class="product-title">Classic Leather Shoes</h3>
                        <strong class="product-price">$120.00</strong>
                        <span class="icon-cross">
                            <img src="images/cross.svg" class="img-fluid" />
                        </span>
                    </a>
                </div>
                <!-- End Column 2 -->


 <!-- Start Column 3 -->
 <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
     <a class="product-item" href="Cart.aspx">
         <img src="images/product-2.png" class="img-fluid product-thumbnail" />
         <h3 class="product-title">Luxury Perfume</h3>
         <strong class="product-price">$85.00</strong>
         <span class="icon-cross">
             <img src="images/cross.svg" class="img-fluid" />
         </span>
     </a>
 </div>
 <!-- End Column 3 -->

            <!-- Start Column 4 -->
            <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                <a class="product-item" href="Cart.aspx">
                    <img src="images/product-3.png" class="img-fluid product-thumbnail" />
                    <h3 class="product-title">Elegant Wrist Watch</h3>
                    <strong class="product-price">$250.00</strong>
                    <span class="icon-cross">
                        <img src="images/cross.svg" class="img-fluid" />
                    </span>
                </a>
            </div>
            <!-- End Column 4 -->
        </div>
    </div>
</div>
<!-- End Product Section -->

<!-- Start Why Choose Us Section -->
<div class="why-choose-section">
    <div class="container">
        <div class="row justify-content-between">
            <div class="col-lg-6">
                <h2 class="section-title">Why Choose Us</h2>
                <p>We provide high-quality, stylish fashion accessories to enhance your personal style. Our commitment to excellence ensures you receive the best products and services.</p>

                <div class="row my-5">
                    <div class="col-6 col-md-6">
                        <div class="feature">
                            <div class="icon">
                                <img src="images/truck.svg" alt="Image" class="imf-fluid" />
                            </div>
                            <h3>Fast &amp; Free Shipping</h3>
                            <p>Enjoy fast and free shipping on all orders. We ensure your new accessories arrive at your doorstep promptly and without any extra cost.</p>
                        </div>
                    </div>

                    <div class="col-6 col-md-6">
                        <div class="feature">
                            <div class="icon">
                                <img src="images/bag.svg" alt="Image" class="imf-fluid" />
                            </div>
                            <h3>Easy to Shop</h3>
                            <p>Our user-friendly website makes it easy to browse, select, and purchase your favorite accessories from the comfort of your home.</p>
                        </div>
                    </div>

                    <div class="col-6 col-md-6">
                        <div class="feature">
                            <div class="icon">
                                <img src="images/support.svg" alt="Image" class="imf-fluid" />
                            </div>
                            <h3>24/7 Support</h3>
                            <p>Our dedicated support team is available 24/7 to assist you with any questions or concerns you may have.</p>
                        </div>
                    </div>

                    <div class="col-6 col-md-6">
                        <div class="feature">
                            <div class="icon">
                                <img src="images/return.svg" alt="Image" class="imf-fluid" />
                            </div>
                            <h3>Hassle Free Returns</h3>
                            <p>We offer hassle-free returns within 30 days of purchase, so you can shop with confidence.</p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-5">
                <div class="img-wrap">
                    <img src="images/why-choose-us-img.png" alt="Image" class="img-fluid" />
                </div>
            </div>
        </div>
    </div>
</div>
<!-- End Why Choose Us Section -->
