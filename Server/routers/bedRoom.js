const router = require('express').Router();
const BedRoom = require('../models/BedRoom')


router.post("/create",async(req,res)=>{
    try {
        const newRoom = new BedRoom({
            homeId: req.body.homeId,
            airConditioningId: req.body.airConditioningId
        })
        const room = await newRoom.save();
        res.status(200).json(room)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.get("/find/:homeId",async(req,res)=>{
    try {
        const room = await BedRoom.findOne({
            homeId : req.params.homeId
        })
        !room&& res.status(403).json("Not found room")
        res.status(200).json(room)
    } catch (error) {
        
    }
})

router.get("/:id/lamp/:status",async(req,res)=>{
    try{
        const room = await BedRoom.findById(req.params.id)
        !room && res.status(403).json("not found home")
        if(req.params.status === "on"){
            room.lamp = true
            await room.save();
            res.status(200).json(room)
        }
        if(req.params.status === "off"){
            room.lamp = false
            await room.save();
            res.status(200).json(room)
        }
    }catch(err){
        res.status(500).json(err)
    }
})
module.exports = router