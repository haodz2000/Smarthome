const router = require('express').Router();
const Home = require('../models/Home')


router.post("/create",async(req,res)=>{
    try {
        const newHome = new Home({
            userId: req.body.userId
        })
        const home = await newHome.save();
        res.status(200).json(home)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.get("/find/:userId",async(req,res)=>{
    try {
        const home = await Home.findOne({
            userId: req.params.userId
        })
        !home && res.status(403).json("Not found Home")
        res.status(200).json(home)
    } catch (error) {
        
    }
})

router.get("/:id/door/:status",async(req,res)=>{
    try{
        const home = await Home.findById(req.params.id)
        !home && res.status(403).json("not found home")
        if(req.params.status === "open"){
            home.door = true
            await home.save();
            res.status(200).json(home)
        }
        if(req.params.status === "closed"){
            home.door = false
            await home.save();
            res.status(200).json(home)
        }
        
        
    }catch(err){
        res.status(500).json(err)
    }
})
module.exports = router