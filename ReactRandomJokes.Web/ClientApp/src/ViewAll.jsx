import React, { useEffect, useState } from 'react';
import axios from 'axios';

const ViewAll = () => {
    const [jokes, setJokes] = useState([]);

    useEffect(() => {
        const getJokes = async () => {
            const { data } = await axios.get('/api/randomjoke/viewall');
            setJokes(data);          
        }
        getJokes();
    }, []);

    return (
        <div className="row">
            <div className="col-md-6 offset-md-3">
                {jokes.map(joke => {
                    return (
                        <div className="card card-body bg-light mb-3">
                            <h5>{joke.setUp}</h5> 
                            <h5>{joke.punchline}</h5>
                            <span>Likes:</span>
                            <br />
                            <span>Dislikes:</span>
                        </div>
                    )
                })}
            </div>
        </div>
    )
}

export default ViewAll;