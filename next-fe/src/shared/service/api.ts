import fetch from "node-fetch";
import { Session, Speaker } from "@/shared/types/Conference";

const apiBaseURL = "https://localhost:44363/api";

const getSessions = (): Promise<Session[]> => {
  const url = `${apiBaseURL}/sessions`;

  return fetch(url).then((res) => res.json());
};

const getSession = (sessionId: number): Promise<Session> => {
  const url = `${apiBaseURL}/sessions/${sessionId}`;

  return fetch(url).then((res) => res.json());
};

const getSpeakers = (): Promise<Speaker[]> => {
  const url = `${apiBaseURL}/speakers`;

  return fetch(url).then((res) => res.json());
};

const getSpeaker = (speakerId: number): Promise<Speaker> => {
  const url = `${apiBaseURL}/speakers/${speakerId}`;

  return fetch(url).then((res) => res.json());
};

const apiService = {
  getSessions,
  getSession,
  getSpeaker,
  getSpeakers,
};

export default apiService;
