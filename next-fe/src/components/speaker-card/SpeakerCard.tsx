import { Speaker } from "@/shared/types/Conference";
import Link from "next/link";

export const SpeakerCard = ({ speaker }: { speaker: Speaker }) => {
  return (
    <div className="card">
      <div className="card-header">
        <Link href="/speakers/[id]" as={`/speakers/${speaker.id}`}>
          <h4 className="card-title">{speaker.name}</h4>
        </Link>
      </div>
      <div className="card-body">
        <p className="card-text">{speaker.bio}</p>
        <br />
        {speaker.webSite && (
          <Link href={speaker.webSite}>WebSite: {speaker.webSite}</Link>
        )}
      </div>
      <div className="card-footer">
        {speaker.sessions &&
          speaker.sessions.map((session) => (
            <Link
              key={session.id}
              href="/sessions/[id]"
              as={`/sessions/${session.id}`}
            >
              {session.title}
            </Link>
          ))}
      </div>
    </div>
  );
};
