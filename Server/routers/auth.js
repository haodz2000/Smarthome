const router = require('express').Router();
const User = require('../models/User')
const bcrypt = require('bcrypt')

router.post("/register",async(req,res)=>{
    try {
        const pass = req.body.password
        const salt = await bcrypt.genSalt(10)
        const hassPassword = await bcrypt.hash(pass,salt)
        const newUser = new User({
            name: req.body.name,
            email: req.body.email,
            password: hassPassword
        })
        const user = await newUser.save()
        const {password, createdAt, updatedAt, ...others} = user._doc
        res.status(200).json(others)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.post("/login",async(req,res)=>{
    try{
        const user = await User.findOne({
            email : req.body.email
        })
        !user&& res.status(403).json("Not find data user")
        const validPassword = await bcrypt.compare(req.body.password, user.password)
        !validPassword && res.status(403).json("Sai tài khoản hoặc mật khẩu")
        const {password, createdAt, updatedAt, ...others} = user._doc
        res.status(200).json(others)
    }catch(err){
        res.status(500).json(err)
    }
})
module.exports = router