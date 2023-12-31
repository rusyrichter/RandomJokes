﻿import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuth } from './RJContextComponent';

const Home = () => {

    const { user } = useAuth();
    const [joke, setJoke] = useState({
        id: '',
        setup: '',
        punchline: '',
        likesCount: '',
        dislikesCount: ''
    })
    const [status, setStatus] = useState('');
   
    useEffect(() => {

        const getJoke = async () => {
            const { data } = await axios.get('/api/randomjoke/getjoke');
            setJoke(data);   
            const { data: interactionStatus } = await axios.get((`/api/randomjoke/getstatus?id=${data.id}`));
            setStatus(interactionStatus);
        }

        getJoke();
    }, [])

    const updateCounts = async () => { 
        if (user) {
            const { data: interactionStatus } = await axios.get((`/api/randomjoke/getstatus?id=${joke.id}`));
            setStatus(interactionStatus);
        }      
        const { data } = await axios.get(`/api/randomJoke/getlikescount/${joke.id}`);
        setJoke({ ...joke, likesCount: data.likes, dislikesCount: data.disLikes });  
    }


    setInterval(updateCounts, 500);



    const onLikeClick = async (liked) => { 
        const JokeId = joke.id;
        console.log(joke)
        await axios.post(`/api/randomJoke/addlike`, { JokeId, liked });       
    }

    const canLike = status != 'Liked';
    const canDislike = status != 'Disliked';

    return (

       
        < div className="container" style={{ marginTop: '60px' }}>

            <div className="row" style={{ minHeight: '80vh', display: 'flex', alignItems: 'center' }}>
                <div className="col-md-6 offset-md-3 bg-light p-4 rounded shadow">
                    <div>
                        <h4>{joke.setup}</h4>
                        <h4>{joke.punchline}</h4>
                        <div>
                            <div>
                                <div>{!user && <a href="/login">Login to your account to like/dislike this joke</a>}</div>
                                {!!user && <button disabled={!canLike} onClick={() => onLikeClick(true)}  className="btn btn-primary">Like</button>}
                                {!!user && <button disabled={!canDislike} onClick={() => onLikeClick(false)}  className="btn btn-danger">Dislike</button>}
                            </div>

                            <br />
                            <h4>Likes:{joke.likesCount} </h4>
                            <h4>Dislikes:{joke.dislikesCount}</h4>
                            <h4>
                                <button className="btn btn-link">Refresh</button>
                            </h4>
                        </div>
                    </div>
                </div>
            </div>
        </div >
    )
}
export default Home;