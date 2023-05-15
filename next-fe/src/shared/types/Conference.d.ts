export interface Track {
  trackID: number;
  conferenceID: number;
  name: string;
}

export interface Speaker {
  id: number;
  name: string;
  bio?: any;
  webSite?: any;
  sessions?: Session[];
}

export interface Session {
  track: Track;
  speakers: Speaker[];
  tags: any[];
  id: number;
  conferenceID: number;
  title: string;
  abstract: string;
  startTime: Date;
  endTime: Date;
  duration: string;
  trackId: number;
}
