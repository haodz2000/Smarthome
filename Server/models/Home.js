const mongoose = require('mongoose')

const HomeSchema = new mongoose.Schema({
    userId:{
        type: String,
        required: true
    },
    temperature:{
        type: Number,
        default: 30,
    },
    weather:{
        type: String,
        default: ""
    },
    humidity:{
        type: Number,
        default: 75,
    },
    door:{
        type: Boolean,
        default: false
        // true: Mở cửa  , false: Đóng cửa,
    }
})

module.exports = mongoose.model("Home",HomeSchema);