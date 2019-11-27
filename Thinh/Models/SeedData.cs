using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thinh.Data;

namespace Thinh.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ProductDbContext context = app.ApplicationServices
                .GetRequiredService<ProductDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        //productId= 1 ,
                        productImgUrl = "https://images-na.ssl-images-amazon.com/images/I/61SVEUsMepL._SL1024_.jpg",
                        productName = "Samsung Galaxy A20 32GB A205G/DS 6.4 HD + 4,"+
                        "000mAh Battery LTE Factory",
                        productUrl = "https://www.amazon.com/Samsung-DS-Unlocked-Smartphone-International/dp/B07QV2JKGQ/ref=sxin_0_ac_d_rm?ac_md=3-3-c2Ftc3VuZyBnYWxheHk%3D-ac_d_rm&crid=101VGUI07M0WW&keywords=phone&pd_rd_i=B07QV2JKGQ&pd_rd_r=0ce275e5-3539-49ea-bca7-1f86ba0c21b3&pd_rd_w=Qdnd7&pd_rd_wg=nx2FJ&pf_rd_p=e2f20af2-9651-42af-9a45-89425d5bae34&pf_rd_r=7WXJGQHR1JTJRGWP4FD4&psc=1&qid=1574825321&sprefix=phone%2Caps%2C236",                       
                        productCategory = "Electronics",
                        productDescription = "Shoot more awesomeness with the 123° Ultra Wide lens."
                        + "Enhance your viewing experience with the next-gen Super AMOLED 16.20cm (6.4”) HD+ Infinity-V Display." +
                        "Quickly power up your phone for the day with the 15W Fast Charging." +
                        "Play games, stream videos and multitask with ease." +
                        "Flaunt the sleek design and the ergonomically placed rear Fingerprint Sensor.",
                        Price = "$178.90",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 2,
                        productImgUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcSt1WHfo9cZnj5-EdIS0rosoqDJeQjXjVc16Hd-RwLqQ8F_Jw56nQ&usqp=CAc",
                        productName = "Google Pixel XL 32GB GSM Unlocked 4G LTE Smartphone in Quite Black",
                        productUrl = "https://www.bestbuy.ca/en-ca/product/google-pixel-xl-32gb-gsm-unlocked-4g-lte-smartphone-in-quite-black-new-in-box/14193690",
                        productCategory = "Electronics",
                        productDescription = "Enjoy a pure Android experience with the unlocked Google Pixel XL G-2PW2100 128GB Smartphone in Quite " +
                        "Black. This phone features an aluminum body and is powered by a quad-core Qualcomm Snapdragon 821 " +
                        "processor with two 2.15 GHz cores and two 1.6 GHz cores as well as 4GB of RAM. It comes equipped with "+
                        "32GB of storage for your media, apps, and more. The reversible USB Type-C interface allows you to connect "+
                        "the device for mass-storage purposes and also serves as a charging port. ",
                        Price = "$499",
                        DateAdded = DateTime.Now
                    }, 
                    new Product
                    {
                        //productId = 3,
                        productImgUrl = "https://www.bell.ca/Styles/wireless/all_languages/all_regions/catalog_images/large/iPhone_11_Pro_Space_Gray_lrg1.png",
                        productName = "Bell Apple iPhone 11 Pro 512GB - Space Grey ",
                        productUrl = "https://www.bestbuy.ca/en-ca/product/bell-apple-iphone-11-pro-512gb-space-grey-select-2-year-agreement/13897367",
                        productCategory = "Electronics",
                        productDescription = "A transformative triple‑camera system that adds tons of capability without complexity. An unprecedented " +
                        "leap in battery life. And a mind‑blowing chip that doubles down on machine learning and pushes the " +
                        "boundaries of what a smartphone can do. Welcome to the first iPhone powerful enough to be called Pro. ",
                        Price = "$949.99",
                        DateAdded = DateTime.Now
                    }, 
                    new Product
                    {
                        //productId = 4,
                        productImgUrl = "https://images-na.ssl-images-amazon.com/images/I/51GZa79nrUL._SX569_.jpg",
                        productName = "Samsung Galaxy Note 10+ 256GB (N975F/DS) - Dual SIM - Aura Glow ",
                        productUrl = "https://www.bestbuy.ca/en-ca/product/samsung-galaxy-note-10-256gb-n975f-ds-dual-sim-aura-glow-unlocked-international-version-w-seller-provided-warranty/13885761",
                        productCategory = "Electronics",
                        productDescription = "Brand New Samsung Galaxy Note 10+ 256GB (SM-N975F/DS) – Dual SIM - GSM Factory Unlocked - Aura " +
                        "Glow. International version with seller-provided warranty. Contact the Seller on your Order Details page to " +
                        "initial a warranty claim. Do not contact the Original Manufacturer. ",
                        Price = "$1,329.99",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 5,
                        productImgUrl = "https://images-na.ssl-images-amazon.com/images/I/71znGoLL%2B4L._SX679_.jpg",
                        productName = "Samsung Galaxy Note 10 Dual Sim Smartphone 256GB 8GB RAM - Black ",
                        productUrl = "https://www.bestbuy.ca/en-ca/product/samsung-galaxy-note-10-dual-sim-smartphone-256gb-8gb-ram-black-unlocked-international-model-w-seller-provided-warranty/13972626",
                        productCategory = "Electronics",
                        productDescription = "Brand new Samsung Galaxy Note 10 Dual Sim Smartphone - Unlocked. International version with seller- " +
                        "provided warranty. Contact the Seller on your Order Details page to initiate a warranty claim. Do not contact " +
                        "the Original Manufacturer. Large 6.3” Infinity Display, Intelligent S Pen, Android 9.0 (Pie) One UI, Triple-lens "+
                        "camera with 2x optical zoom, Non-removable Li-Ion 3500 mAh battery ",
                        Price = "$1,049.99",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 6,
                        productName = "2006 Mercedes-Benz CLK ",
                        productCategory = "Vehicles",
                        productUrl = "https://www.carpages.ca/used-cars/quebec/laval/2006-mercedes-benz-clk-4230390/",                   
                        productDescription = "The 2006 Mercedes-Benz CLK-Class is a stylish, if expensive, melding of comfort and performance suitable for all but the most serious driving enthusiasts. ",
                        productImgUrl = "https://images.carpages.ca/inventory/4230390.125944824?w=1280&h=960&q=75&fit=max&s=fedcbc9981f7b062fa8a1adaaea51985",                     
                        Price = "$7,495",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 7,
                        productName = "2016 Kia Soul ",
                        productCategory = "Vehicles",
                        productUrl = "https://www.carpages.ca/used-cars/quebec/ste-julie/2016-kia-soul-4229163/",
                        productDescription = "The 2016 Kia Soul's funky styling, fun-to-drive attitude, extensive feature content and wagon-based practicality make it a great choice for an urban commuter or as an alternative to a small crossover. ",
                        productImgUrl = "https://images.carpages.ca/inventory/4229163.125913741?w=1280&h=960&q=75&fit=max&s=4889c4042f7ab60debcd8ea573ebd6c3",
                        Price = "$15,995",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 8,
                        productImgUrl = "https://images.carpages.ca/inventory/4223463.125798022?w=1280&h=960&q=75&fit=max&s=6932d273b6419eb2c0a35cd0f991e03c",
                        productName = "2018 Toyota Camry ",
                        productUrl = "https://www.carpages.ca/used-cars/quebec/sherbrooke/2018-toyota-camry-4223463/",
                        productCategory = "Vehicles",
                        productDescription = "The 2018 Toyota Camry ranks among the best midsize sedans thanks to an abundance of standard features, pleasant driving dynamics, and a powerful yet fuel-efficient base engine. ",
                        Price = "$36,995",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 9,
                        productImgUrl = "https://images.carpages.ca/inventory/4230504.125948679?w=1280&h=960&q=75&fit=max&s=e999eb6179f32bfd5657386893238712",
                        productName = "2015 Jeep Cherokee ",
                        productUrl = "https://www.carpages.ca/used-cars/quebec/varennes/2015-jeep-cherokee-4230504/",
                        productCategory = "Vehicles",
                        productDescription = "The 2015 Jeep Cherokee is a five-seat compact SUV offered in four trim levels: Sport, Latitude, Limited and Trailhawk. Front-wheel drive is standard and can be upgraded to four-wheel drive on all but the Trailhawk, which has four-wheel drive standard. The Jeep Cherokee competes with other off-roaders like the Nissan Xterra and Toyota 4Runner, as well as the Chevrolet Equinox and Subaru Outback. ",
                        Price = "$15,999",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 10,
                        productImgUrl = "https://images.carpages.ca/inventory/4230492.125948247?w=1280&h=960&q=75&fit=max&s=3f5ac0600bfde2005bccca9b518b5631",
                        productName = "2014 Toyota RAV4 ",
                        productUrl = "https://www.carpages.ca/used-cars/quebec/brossard/2014-toyota-rav4-4230492/",
                        productCategory = "Vehicles",
                        productDescription = "The 2014 Toyota RAV4 is a five-seat compact SUV that competes with the Honda CR-V, Ford Escape and Mazda CX-5, among others. Trim levels include the LE, XLE and Limited. All come with front-wheel drive or optional all-wheel drive. ",
                        Price = "$17,995",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 11,
                        productImgUrl = "https://cdn.shopify.com/s/files/1/0472/3357/products/wayfarer_front_2048x.png",
                        productName = "Ray-Ban New Wayfarer",
                        productDescription = "Go back to where it all began with Ray-Ban New Wayfarer Classic sunglasses. Using the same iconic shape as the classic Wayfarer, these sunglasses offer an updated version that includes a smaller frame and slightly softer eye shape.",
                        productCategory = "Accessory",
                        productUrl = "https://www.ray-ban.com/canada/en/sunglasses/RB2132%20UNISEX%20047-new%20wayfarer%20classic-gloss%20black/805289052418",
                        Price = "177.99$",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 12,
                        productImgUrl = "https://cdn.shopify.com/s/files/1/0472/3357/products/wayfarer_front_2048x.png",
                        productName = "Men's Gore CloseFit Fleece Gloves",
                        productDescription = "Say goodbye to cold hands that are of little to no use; these lightweight and streamlined multi-season gloves feature an ultra-durable, windproof, three-layer shell and a soft, brushed-fleece lining for versatile warmth during wintry hikes. Now, you’ll actually be able to turn your keys in the ignition once you get back to the car.",
                        productCategory = "Accessory",
                        productUrl = "https://www.thenorthface.com/shop/mens-gore-closefit-fleece-gloves-nf0a3kp8",
                        Price = "50.00$",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 13,
                        productImgUrl = "https://cdn.shopify.com/s/files/1/0133/7012/products/MEC-Ti5Wallet-BLK-Main.jpg",
                        productName = "Ti5 Slim Wallet | Black DLC",
                        productDescription = "This is the Machine Era Ti5 Wallet + Black DLC PVD Coating. Ultra-light, ultra-durable, super-svelte, and bottle-opening – it's the slimmest, lightest, and strongest wallet you’ll find, and an all-around cornerstone to your everyday carry.",
                        productCategory = "Accessory",
                        productUrl = "https://www.machine-era.com/collections/frontpage/products/ti5-slim-wallet-dlc",
                        Price = "65.00$",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId = 14,
                        productImgUrl = "https://res.cloudinary.com/guess-img/image/upload/c_fill,dpr_3.0,f_auto,g_auto,h_1200,q_auto,w_800/c_fill,h_1200,w_800/v1/NA/Style/ECOMM/DX19101-BLUSH",
                        productName = "Julina Quilted Crossbody",
                        productDescription = "Give your look a polished finish with this faux-leather crossbody, featuring quilted details, a logo emblem and rhinestone-embellished charms for added glam. Back pocket. Fold-over flap with magnetic snap-button closure and zippered interior pocket.",
                        productCategory = "Accessory",
                        productUrl = "https://www.guessfactory.ca/en/catalog/view/women/handbags/view-all/julina-quilted-crossbody/dx-19-101?color=blush",
                        Price = "54.99$",
                        DateAdded = DateTime.Now
                     },
                    new Product
                    {
                        //productId = 15,
                        productImgUrl = "https://images.canadagoose.com/image/upload/v1563815808/product-image/5816MP_831.jpg",
                        productName = "Forester Jacket Print",
                        productDescription = "The Forester Jacket is crafted from our durable Arctic Tech® fabric and cut in a shorter length, making it an easy top layer to throw on and go. It's detailed with military-inspired exterior pockets to stow the smaller items, and finished with a double-layered collar for extra protection around the face.",
                        productCategory = "Accessory",
                        productUrl = "https://www.canadagoose.com/ca/en/forester-jacket-print-5816MP.html",
                        Price = "850.00$",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId= 16 ,
                        productImgUrl = "https://cdn.shopify.com/s/files/1/2660/5106/products/zwaybzrube4i3yrukfq2_1400x.jpg",
                        productName = "Tessaro Pop-Up Sofabed - Charcoal",
                        productDescription = "With the Tessaro Pop-Up Sofabed, you have everything at your fingertips — from a convenient guest bed to spacious, hidden storage and a practical, comfortable sofa for the whole family. Featuring easy-care, polyester fabric in a dark charcoal hue, the Tessaro simultaneously delivers impressive functionality and rich, contemporary style.",
                        productCategory = "Furniture",
                        productUrl = "https://www.leons.ca/products/tessaro-pop-up-sofabed-charcoal",
                        Price = "799.00$",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId= 17 ,
                        productImgUrl = "https://www.sleepfactory.com/wp-content/uploads/2019/06/2206_Madrid-Metal-Fabric-Platform-Bed-REG.jpg",
                        productName = "Madrid Metal Upholstered Platform Bed",
                        productDescription = "The Madrid platform bed has a bold, contemporary design that will help create a sleeping area sure to impress. With sharp lines, metal frame and nailheads on the grey linen-style fabric. Centre supports, metal slat system. No box spring required.",
                        productCategory = "Furniture",
                        productUrl = "https://www.sleepfactory.com/product/madrid-metal-upholstered-platform-bed/",
                        Price = "199.00$",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId= 18 ,
                        productImgUrl = "https://cdn.shopify.com/s/files/1/2660/5202/products/e6l8w8byazjbfqb5pmeb_1400x.jpg",
                        productName = "Talia 5-Piece Dining Package",
                        productDescription = "Start your family traditions around a classic set of furniture like this Talia five-piece dining package. At the centre of the mix is the thickset, solid wood table. It extends an extra 18 inches thanks to the removable leaf. The rest of the wood surfaces are capped with lustrous mango veneers, known for their beautiful wood grain and a tendency to age gracefully. Around the table is a set of matching dining chairs. Wrapped in resilient faux leather and generously padded, no one will be in too much of a hurry to abandon their seats.",
                        productCategory = "Furniture",
                        productUrl = "https://www.thebrick.com/products/talia-5-piece-dining-package",
                        Price = "749.98$",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId= 19 ,
                        productImgUrl = "https://www.ikea.com/ca/en/images/products/lots-mirror__0344836_PH119895_S5.JPG",
                        productName = "LOTS",
                        productDescription = "Filling a wall with mirrors is a simple way to make a small room feel larger. Add as many squares as you want to create the shape and size of mirror you want.",
                        productCategory = "Furniture",
                        productUrl = "https://www.ikea.com/ca/en/p/lots-mirror-39151700/",
                        Price = "9.99$",
                        DateAdded = DateTime.Now
                    },
                    new Product
                    {
                        //productId= 20 ,
                        productImgUrl = "https://www.structube.com/media/catalog/product/cache/1/thumbnail/900x698/f118ec6a7f8417e96ed53a398f5e2e6d/0/1/01-97.38.50.97_coffeetable_mika-97.38.50.97.jpg",
                        productName = "MIKA Coffee table",
                        productDescription = "With Mika you can have it all. A refined design featuring a ceramic top, providing an elegant marble look, ample storage, and a considerate footprint. Place Mika at the center of your living space for a fresh pop of modern excellence.",
                        productCategory = "Furniture",
                        productUrl = "https://www.structube.com/en/mika-coffee-table-110cm-43-97-38-50?pid=27632",
                        Price = "329.00$",
                        DateAdded = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
